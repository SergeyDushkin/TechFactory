using System;

namespace TF.Data.Business
{
    public class Unit
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }

        public Guid? ParentId { get; set; }
    }
}
