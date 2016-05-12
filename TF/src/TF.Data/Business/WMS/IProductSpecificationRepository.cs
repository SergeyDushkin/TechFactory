using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business.WMS
{
    public interface IProductSpecificationRepository
    {
        Task<ProductSpecification> CreateAsync(ProductSpecification specification);
        Task<ProductSpecification> UpdateAsync(ProductSpecification specification);
        Task DeleteAsync(Guid id);
        Task<ProductSpecification> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductSpecification>> GetByParentIdAsync(Guid id);
        Task<IEnumerable<ProductSpecification>> GetByChildIdAsync(Guid id);
    }
}
