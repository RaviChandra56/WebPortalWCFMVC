using WebPortal.Shared.ServiceContracts.Service;

namespace WebPortal.Shared.ServiceClients
{
    public class CalculatorServiceClient : ServiceClientBase<ICalculatorService>, ICalculatorService
    {
        public const string endpointName = "ICalculatorService";
        public int Add(int firstValue, int secondValue)
        {
            return (int)base.Invoke(((ICalculatorService proxy) => proxy.Add(firstValue, secondValue)), endpointName);
        }

        public int Multiply(int firstValue, int secondValue)
        {
            return (int)base.Invoke(((ICalculatorService proxy) => proxy.Multiply(firstValue, secondValue)), endpointName);
        }
    }
}
