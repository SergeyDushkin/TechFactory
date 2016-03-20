using System;
using System.Collections.Generic;

namespace TF.Data.Business.WMS
{
    public interface IProductCategoryService
    {
        ProductCategory GetById(Guid id);
        IEnumerable<ProductCategory> GetByCategoryId(Guid id);
        IEnumerable<ProductCategory> GetByProductId(Guid id);
        ProductCategory Create(ProductCategory product);
        ProductCategory Update(ProductCategory product);
        void Delete(Guid id);
    }
}
