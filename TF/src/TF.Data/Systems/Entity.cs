using System;

namespace TF.Data.Systems
{
    public class Entity
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// to build relashinship between entites, 
        /// in future it will be hepfull for example to have integration with different data providers
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
