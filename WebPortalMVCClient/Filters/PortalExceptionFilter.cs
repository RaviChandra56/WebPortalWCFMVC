using System;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebPortalMVCClient.Models;

namespace WebPortalMVCClient.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class PortalExceptionFilter : FilterAttribute, IExceptionFilter 
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }
            
            if (filterContext.Exception is HttpException)
            {
                HttpException httpException = filterContext.Exception as HttpException;
                filterContext.Result = new HttpStatusCodeResult(httpException.GetHttpCode(), httpException.Message);
                return;
            }

            filterContext.ExceptionHandled = true;

            if (filterContext.Exception is MessageSecurityException)
            {
                filterContext.Result =
                new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            action = "InValidUser",
                            controller = "Error",
                            area = ""
                        }
                    )
                );
                return;
            }

            if (filterContext.Exception is SecurityAccessDeniedException)
            {
                filterContext.Result =
                new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            action = "AccessDenied",
                            controller = "Error",
                            area = ""
                        }
                    )
                );
                return;
            }


            if (IsJsonRequest(filterContext.HttpContext.Request))
            {
                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                action = "JsonError",
                                controller = "Error",
                                area = ""
                            }
                        )
                    );
                filterContext.ExceptionHandled = true;
                return;
            }

            ErrorMessage error = new ErrorMessage();
            error.Message = filterContext.Exception.Message;

            if (filterContext.Exception.InnerException != null)
            {
                error.AdditionalInformation = filterContext.Exception.InnerException.Message;
            }

            ///Write Log

            filterContext.Result =
                new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            action = "Index",
                            controller = "Error",
                            area = ""
                        }
                    )
                );
            filterContext.ExceptionHandled = true;
        }

        // Found here: http://stackoverflow.com/questions/6223063/whats-the-best-way-to-detect-a-json-request-on-asp-net

        public static bool IsJsonRequest(HttpRequestBase request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            bool rtn = false;
            const string jsonMime = "application/json";

            if (request.AcceptTypes != null)
            {
                rtn = request.AcceptTypes.Any(t => t.Equals(jsonMime, StringComparison.OrdinalIgnoreCase));
            }

            return rtn || request.ContentType.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Any(t => t.Equals(jsonMime, StringComparison.OrdinalIgnoreCase));
        }
    }
}