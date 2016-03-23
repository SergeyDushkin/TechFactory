using System;
using System.Collections.Generic;

namespace TF.Data.Business.WMS
{
    public interface IProductPriceService
    {
        ProductPrice Create(ProductPrice price);
        ProductPrice Update(ProductPrice price);

        void Delete(Guid id);
        void DeleteByProduct(Guid id);

        ProductPrice GetByProductId(Guid id);
        ProductPrice GetById(Guid id);
    }
}
