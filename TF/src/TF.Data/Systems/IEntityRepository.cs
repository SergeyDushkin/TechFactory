using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Systems
{
    public interface IEntityRepository
    {
        Entity Create(Entity unit);
        Task<Entity> CreateAsync(Entity unit);

        Entity Update(Entity unit);
        Task<Entity> UpdateAsync(Entity unit);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);
        
        IEnumerable<Entity> GetByParentId(Guid id);
        Entity GetById(Guid id);
        
        Task<IEnumerable<Entity>> GetByParentIdAsync(Guid id);
        Task<Entity> GetByIdAsync(Guid id);

        IEnumerable<Entity> GetAll();
        Task<IEnumerable<Entity>> GetAllAsync();
    }
}
