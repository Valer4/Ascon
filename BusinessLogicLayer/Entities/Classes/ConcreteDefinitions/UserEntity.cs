using System.Collections.Generic;

namespace BusinessLogicLayer.Entities.Classes.ConcreteDefinitions
{
    public class UserEntity : AbstractEntity<long>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public List<RoleEntity> Roles { get; set; }
    }
}
