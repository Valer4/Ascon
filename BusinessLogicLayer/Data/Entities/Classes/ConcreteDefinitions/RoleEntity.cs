using System.Collections.Generic;

namespace BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions
{
    public class RoleEntity : AbstractEntity<long>
    {
        public string Name { get; set; }

        public List<UserEntity> Users { get; set; }
    }
}
