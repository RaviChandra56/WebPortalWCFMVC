using System.ServiceModel;

namespace WebPortal.Shared.ServiceContracts.Service
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        int Add(int firstValue, int secondValue);
        [OperationContract]
        int Multiply(int firstValue, int secondValue);
    }
}
