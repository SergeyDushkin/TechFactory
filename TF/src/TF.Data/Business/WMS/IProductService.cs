using System;
using System.Collections.Generic;

namespace TF.Data.Business.WMS
{
    public interface IProductService
    {
        Product Get(Guid productId);
        Product Save(Product product);
        IEnumerable<Product> Get();
        void Delete(Guid id);
    }
}
