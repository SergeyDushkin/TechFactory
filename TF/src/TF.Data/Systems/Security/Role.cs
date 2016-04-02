using System;

namespace TF.Data.Systems.Security
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }

        public Guid? BatchGuid { get; set; }
        public Boolean Hidden { get; set; }
        public Boolean Deleted { get; set; }
    }
}
