using System;

namespace TF.Data.Systems.Security
{
    public class User
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public DateTime LastLogin { get; set; }
        public Int16 LoginAttemptCount { get; set; }

        public Guid? BatchGuid { get; set; }
        public Boolean Hidden { get; set; }
        public Boolean Deleted { get; set; }

    }    
}
