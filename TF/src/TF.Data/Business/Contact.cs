using System;

namespace TF.Data.Business
{
    public class Contact
    {
        public Guid Id { get; set; }
        //public Guid ENTITY_GUID { get; set; }
        public Guid RecordGuid { get; set; }
        public string Type { get; set; }

        public Guid? BatchGuid { get; set; }
        public Boolean Hidden { get; set; }
        public Boolean Deleted { get; set; }
    }
}
