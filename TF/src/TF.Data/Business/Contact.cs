using System;

namespace TF.Data.Business
{
    public class CONTACT
    {
        public Guid GUID_RECORD { get; set; }
        //public Guid ENTITY_GUID { get; set; }
        public Guid RECORD_GUID { get; set; }
        public string TYPE { get; set; }

        public Guid? BATCH_GUID { get; set; }
        public Boolean HIDDEN { get; set; }
        public Boolean DELETED { get; set; }
    }
}
