using System;
using System.Collections.Generic;

namespace TF.Data.Business.WMS
{
    public interface IProductPriceService
    {
        void Create(ProductPrice price);
        void Update(ProductPrice price);

        void Delete(Guid id);
        void DeleteByProduct(Guid id);

        ProductPrice GetByProductId(Guid id);
        ProductPrice GetById(Guid id);
    }
}
