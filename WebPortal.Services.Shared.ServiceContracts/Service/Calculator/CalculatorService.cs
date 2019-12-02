using System;
using System.Security.Permissions;

namespace WebPortal.Shared.ServiceContracts.Service
{
    public class CalculatorService : ICalculatorService
    {
        [PrincipalPermission(SecurityAction.Demand,Role ="Admin")]
        public int Add(int firstValue, int secondValue)
        {
            try
            {
                return firstValue + secondValue;
            }
            catch (Exception ex)
            {
                throw new Exception("Access Denied");
            }
        }

        public int Multiply(int firstValue, int secondValue)
        {
            try
            {
                return firstValue * secondValue;
            }
            catch(Exception ex)
            {
                throw new Exception("Access Denied");
            }
        }
    }
}
