using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace WebPortal.Shared.ServiceClients
{
    public abstract class ServiceClientBase<TService>
    {
        public ServiceClientBase()
        {

        }

        public object Invoke(Expression<Func<TService, object>> expressionToCall,
                                       string endpointConfigurationName)
        {
            Func<TService, object> methodToCall = expressionToCall.Compile();

            ChannelFactory<TService> channel = new ChannelFactory<TService>(endpointConfigurationName);
            // instantiate credentials
            // step one - find and remove default endpoint behavior 
            var defaultCredentials = channel.Endpoint.Behaviors.Find<ClientCredentials>();
            channel.Endpoint.Behaviors.Remove(defaultCredentials);

            // step two - instantiate your credentials
            ClientCredentials loginCredentials = new ClientCredentials();
            loginCredentials.UserName.UserName = "test";
            loginCredentials.UserName.Password = "test";

            // step three - set that as new endpoint behavior on factory
            channel.Endpoint.Behaviors.Add(loginCredentials); //add required ones

            // Get Calling method (Client or ClientInstance method)
            var methodBase = new StackTrace(1).GetFrame(0).GetMethod();

            if (methodBase.Name == "Invoke")
                methodBase = new StackTrace(2).GetFrame(0).GetMethod();

            var methodInfo = methodBase as MethodInfo;
            object result = methodInfo == null ? null : GetDefaultValue(methodInfo.ReturnType);
            TService proxy = channel.CreateChannel();

            try
            {
                result = methodToCall(proxy);
            }
            catch (MessageSecurityException ex)
            {
                throw ex;
            }
            catch (SecurityAccessDeniedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        private object GetDefaultValue(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }
    }
}
