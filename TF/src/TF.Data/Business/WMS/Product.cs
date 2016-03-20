using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TF.Data.Business.WMS
{
    public class Product
    {
        [Column("GUID_RECORD")]
        public Guid Id { get; set; }

        /// <summary>
        /// REGULAR, KIT, DYNAKIT, NONSTOCK, INGREDIENT
        /// </summary>
        [Column("TYPE")]
        public string Type { get; set; }

        [Column("KEY")]
        public string Key { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        public virtual Product Parent { get; set; }

        public ICollection<Product> ChildProducts { get; set; }
    }
}
