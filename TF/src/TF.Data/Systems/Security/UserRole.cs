using System;

namespace TF.Data.Systems.Security
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public Guid UserGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public Guid? BatchGuid  { get; set; }
        public Boolean Hidden { get; set; }
        public Boolean Deleted { get; set; }
    }
}

