using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WebPortal.Shared.ServiceClients;
using WebPortal.Shared.ServiceContracts.Service;

namespace WebPortalMVCClient
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ICalculatorService, CalculatorServiceClient>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}