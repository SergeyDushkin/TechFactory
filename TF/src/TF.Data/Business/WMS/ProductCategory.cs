using System;

namespace TF.Data.Business.WMS
{
    public class ProductCategory
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
    }
}
