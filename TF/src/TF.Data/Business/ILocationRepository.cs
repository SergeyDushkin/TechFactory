using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface ILocationRepository
    {
        Location Create(Location location);
        Task<Location> CreateAsync(Location location);

        Location Update(Location location);
        Task<Location> UpdateAsync(Location location);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        Location GetById(Guid id);
        Task<Location> GetByIdAsync(Guid id);

        IEnumerable<Location> GetAll();
        Task<IEnumerable<Location>> GetAllAsync();
    }
}
