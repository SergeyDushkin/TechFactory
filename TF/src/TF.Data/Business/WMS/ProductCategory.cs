using System;
using System.Collections.Generic;
using TF.Data.Systems;

namespace TF.Data.Business.WMS
{
    public class ProductCategory
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }

        public virtual ICollection<Link> Links { get; set; }
    }
}
