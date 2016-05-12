using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TF.Data.Systems;

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
        public ICollection<ProductSpecification> Ingredients { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductPrice Price { get; set; }
        
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Link> Links { get; set; }
    }
}
