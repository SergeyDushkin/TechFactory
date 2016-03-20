using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface ICategoryService
    {
        Category Create(Category category);
        Task<Category> CreateAsync(Category category);

        Category Update(Category category);
        Task<Category> UpdateAsync(Category category);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetByParentId(Guid id);
        Category GetById(Guid id);

        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetByParentIdAsync(Guid id);
        Task<Category> GetByIdAsync(Guid id);
    }
}
