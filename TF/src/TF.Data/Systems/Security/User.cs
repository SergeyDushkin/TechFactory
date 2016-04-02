using System;

namespace TF.Data.Systems.Security
{
    public class USER
    {
        public Guid ID { get; set; }
        public string KEY { get; set; }
        public DateTime LAST_LOGIN { get; set; }
        public Int16 LOGIN_ATTEMPT_COUNT { get; set; }

        public Guid? BATCH_GUID { get; set; }
        public Boolean HIDDEN { get; set; }
        public Boolean DELETED { get; set; }

    }    
}
