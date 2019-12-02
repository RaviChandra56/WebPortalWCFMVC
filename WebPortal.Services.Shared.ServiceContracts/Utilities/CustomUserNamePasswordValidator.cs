using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace WebPortal.Shared.ServiceContracts.Utilities
{
    /// <summary>
    /// UserNamePasswordValidator == abstract class
    /// </summary>
    public class CustomUserNamePasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName == "test" && password == "test")
            {
                return;
            }

            throw new SecurityTokenException("Access Denied");
        }
    }
}
