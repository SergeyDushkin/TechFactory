using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface IUnitRepository
    {
        Unit Create(Unit unit);
        Task<Unit> CreateAsync(Unit unit);

        Unit Update(Unit unit);
        Task<Unit> UpdateAsync(Unit unit);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        IEnumerable<Unit> GetAll();
        IEnumerable<Unit> GetByParentId(Guid id);
        Unit GetById(Guid id);

        Task<IEnumerable<Unit>> GetAllAsync();
        Task<IEnumerable<Unit>> GetByParentIdAsync(Guid id);
        Task<Unit> GetByIdAsync(Guid id);
    }
}
