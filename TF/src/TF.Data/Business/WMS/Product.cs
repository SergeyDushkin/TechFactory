using System;
using System.Collections.Generic;

namespace TF.Data.Business.WMS
{
    public class Product
    {
        public Guid Id { get; set; }

        /// <summary>
        /// REGULAR, KIT, DYNAKIT, NONSTOCK, INGREDIENT
        /// </summary>
        public string Type { get; set; }

        public string Name { get; set; }

        public virtual Product Parent { get; set; }

        public ICollection<Product> ChildProducts { get; set; }
    }
}
