using System;
using System.Collections.Generic;

namespace TF.Data.Business.WMS
{
    public interface IProductRepository
    {
        Product GetById(Guid id);
        Product GetByKey(string key);
        Product Create(Product product);
        Product Update(Product product);
        IEnumerable<Product> Get();
        void Delete(Guid id);
    }
}
