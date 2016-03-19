using System;
using System.Collections.Generic;

namespace TF.Data.Business.WMS
{
    public interface IProductCategoryService
    {
        ProductCategory Get(Guid productId);
        ProductCategory Save(ProductCategory product);
        IEnumerable<ProductCategory> Get();
        void Delete(Guid id);
    }
}
