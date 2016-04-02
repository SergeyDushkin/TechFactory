using System;

namespace TF.Data.Systems.Security
{

    public class UserIdentity
    {
        public Guid Id { get; set; }
        public string Provider { get; set; }
        public string Key { get; set; }

        public Guid? BatchGuid { get; set; }
        public Boolean Hidden { get; set; }
        public Boolean Deleted { get; set; }
    }
}
