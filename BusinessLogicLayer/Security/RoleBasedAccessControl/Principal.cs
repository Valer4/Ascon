using System.Linq;
using System.Security.Principal;

namespace BusinessLogicLayer.Security.RoleBasedAccessControl
{
    internal class Principal : IPrincipal
    {
        public IIdentity Identity
        {
            get;
            private set;
        }

        private string[] _roles;

        public Principal(IIdentity identity) => Identity = identity;

        public bool IsInRole(string role)
        {
            EnsureRoles();

            return _roles.Contains(role);
        }
        private void EnsureRoles()
        {
            if (_roles != null) return;

            _roles = new RoleProvider().GetRolesForUser(Identity.Name);
        }
    }
}
