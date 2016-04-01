using System;

namespace TF.Data.Business
{
    public class CONTACT_DETAIL
    {
        public Guid GUID_RECORD { get; set; }
        public Guid CONTACT_GUID { get; set; }
        public Int16 PRIORITY { get; set; }
        public string TYPE { get; set; }
        public string VALUE { get; set; }
        public Boolean VERIFIED { get; set; }
        public Boolean ALLOW { get; set; }

        public Guid? BATCH_GUID { get; set; }
        public Boolean HIDDEN { get; set; }
        public Boolean DELETED { get; set; }
    }
}
