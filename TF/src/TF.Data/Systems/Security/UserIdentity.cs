using System;

namespace TF.Data.Systems.Security
{

    public class USER_IDENTITY
    {
        public Guid USER_GUID { get; set; }
        public string PROVIDER { get; set; }
        public string KEY { get; set; }
        public Guid? BATCH_GUID { get; set; }
        public Boolean HIDDEN { get; set; }
        public Boolean DELETED { get; set; }
    }
}
