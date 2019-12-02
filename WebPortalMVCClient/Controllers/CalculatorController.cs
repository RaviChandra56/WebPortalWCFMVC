using System.Web.Mvc;
using WebPortal.Shared.ServiceContracts.Service;
using WebPortalMVCClient.Filters;

namespace WebPortalMVCClient.Controllers
{
    [PortalExceptionFilter]
    public class CalculatorController : Controller
    {
        ICalculatorService _calculatorService;
        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        // GET: Calculator
        public ActionResult Index()
        {
            return Content(_calculatorService.Add(10,10).ToString());
        }
    }
}