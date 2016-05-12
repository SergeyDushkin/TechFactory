using System;

namespace TF.DAL.Models
{
    class BUSINESS_WMS_KIT_SPEC
    {
        public Guid GUID_RECORD { get; set; }
        public Guid PARENT_GUID { get; set; }
        public Guid CHILD_GUID { get; set; }
        public Guid CHILD_UOM_GUID { get; set; }
        public float BASE_QTY { get; set; }
        public Guid? BATCH_GUID { get; set; }
        public bool HIDDEN { get; set; }
        public bool DELETED { get; set; }
    }
}