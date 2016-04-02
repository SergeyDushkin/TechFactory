using System;

namespace TF.Data.Systems.Security
{
    public class ROLE
    {
        public Guid ID { get; set; }
        public string KEY { get; set; }
        public string NAME { get; set; }
        public Guid? BATCH_GUID { get; set; }
        public Boolean HIDDEN { get; set; }
        public Boolean DELETED { get; set; }
    }
}
