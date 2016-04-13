using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TF.Data.Business
{
    public class Location
    {
        public Guid Id { get; set; }

        /// <summary>
        /// fixed values: STORE, WAREHOUSE, OFFICE, HOME, WORK, MAIN
        /// </summary>
        public string Type { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }

        public Guid? UnitId { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
