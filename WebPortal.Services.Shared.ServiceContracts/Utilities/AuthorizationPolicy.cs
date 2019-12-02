using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;

namespace WebPortal.Shared.ServiceContracts.Utilities
{
    public class CustomPrincipal : IPrincipal
    {
        private IIdentity _Identity;
        public IIdentity Identity
        {
            get { return _Identity; }
        }

        public bool IsInRole(string role)
        {
            string UserName = Identity.Name.ToString();
            if (UserName == "test" && role == "User")
                return true;
            else
                return false;
        }

        public CustomPrincipal(IIdentity identity)
        {
            // TODO: Complete member initialization
            _Identity = identity;
        }
    }

    public class AuthorizationPolicy : IAuthorizationPolicy
    {
        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        public string Id {
            get { return Guid.NewGuid().ToString(); }
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                return false;

            IList<IIdentity> identities = obj as IList<IIdentity>;
            if (obj == null || identities.Count <= 0)
                return false;

            evaluationContext.Properties["Principal"] = new CustomPrincipal(identities[0]);
            return true;
        }
    }
}
