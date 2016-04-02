using System;

namespace TF.Data.Systems.Security
{
    public class USER_ROLE
    {
        public Guid ID { get; set; }
        public Guid USER_GUID { get; set; }
        public Guid ROLE_GUID { get; set; }
        public Guid? BATCH_GUID { get; set; }
        public Boolean HIDDEN { get; set; }
        public Boolean DELETED { get; set; }
    }
}

