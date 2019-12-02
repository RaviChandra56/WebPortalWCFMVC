using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPortalMVCClient.Models;

namespace WebPortalMVCClient.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            ErrorMessage objErrorMessage = new ErrorMessage();
            objErrorMessage.Message = "Error Occured";
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View("Error", objErrorMessage);
        }

        public JsonResult JsonError()
        {
            string errMessage = "Invalid Ajax Call";
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Json(errMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InValidUser()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}