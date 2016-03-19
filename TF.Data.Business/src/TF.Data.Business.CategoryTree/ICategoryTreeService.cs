using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface ICategoryTreeService
    {
        void Create(CategoryTree category);
        Task CreateAsync(CategoryTree category);

        void Update(CategoryTree category);
        Task UpdateAsync(CategoryTree category);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        IEnumerable<CategoryTree> GetAll();
        IEnumerable<CategoryTree> GetByParentId(Guid id);
        CategoryTree GetById(Guid id);

        Task<IEnumerable<CategoryTree>> GetAllAsync();
        Task<IEnumerable<CategoryTree>> GetByParentIdAsync(Guid id);
        Task<CategoryTree> GetByIdAsync(Guid id);
    }
}
