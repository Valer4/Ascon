using System.Collections.Generic;

namespace BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions
{
    public class UserEntity : AbstractEntity<long>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<RoleEntity> Roles { get; set; }
    }
}
