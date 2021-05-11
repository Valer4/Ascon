using BusinessLogicLayer.Entities;
using System.Collections.Generic;

namespace BusinessLogicLayer.Entities.ConcreteDefinitions
{
    public class RoleEntity : AbstractEntity<long>
    {
        public string Name { get; set; }

        public List<UserEntity> Users { get; set; }
    }
}
