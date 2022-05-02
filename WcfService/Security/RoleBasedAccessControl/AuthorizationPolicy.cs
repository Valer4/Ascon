using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Principal;

namespace WcfService.Security.RoleBasedAccessControl
{
    internal class AuthorizationPolicy : IAuthorizationPolicy
    {
        private string _id = Guid.NewGuid().ToString();
        public string Id
        {
            get { return _id; }
        }

        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        public bool Evaluate(EvaluationContext context, ref object state)
        {
            IIdentity client = GetClientIdentity(context);

            context.Properties["Principal"] = new Principal(client);

            return true;
        }

        private IIdentity GetClientIdentity(EvaluationContext context)
        {
            object obj;
            if ( ! context.Properties.TryGetValue("Identities", out obj))
                throw new Exception("No Identity found");

            IList<IIdentity> identities = obj as IList<IIdentity>;
            if (null == identities || ! identities.Any())
                throw new Exception("No Identity found");

            return identities[0];
        }
    }
}
