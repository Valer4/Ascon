using System.Linq;
using System.Security.Principal;

namespace WcfService.Security.RoleBasedAccessControl
{
    internal class Principal : IPrincipal
    {
        public IIdentity Identity
        {
            get;
            private set;
        }

        private string[] _roles;

        internal Principal(IIdentity identity) => Identity = identity;

        public bool IsInRole(string role)
        {
            EnsureRoles();

            return _roles.Contains(role);
        }
        private void EnsureRoles()
        {
            if (null != _roles) return;

            _roles = new RoleProvider().GetRolesForUser(Identity.Name);
        }
    }
}
