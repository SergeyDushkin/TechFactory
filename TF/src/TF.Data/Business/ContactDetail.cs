using System;

namespace TF.Data.Business
{
    public class ContactDetail
    {
        public Guid Id { get; set; }
        public Guid ContactGuid { get; set; }
        public Int16 Priority { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Boolean Verified { get; set; }
        public Boolean Allow { get; set; }

        public Guid? BatchGuid { get; set; }
        public Boolean Hidden { get; set; }
        public Boolean Deleted { get; set; }
    }
}
