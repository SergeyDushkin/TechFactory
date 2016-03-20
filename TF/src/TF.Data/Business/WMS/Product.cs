using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TF.Data.Business.WMS
{
    public class Product
    {
        public Guid Id { get; set; }

        /// <summary>
        /// REGULAR, KIT, DYNAKIT, NONSTOCK, INGREDIENT
        /// </summary>
        public string Type { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }

        public virtual Product Parent { get; set; }

        public ICollection<Product> ChildProducts { get; set; }
    }
}
