using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Systems
{
    public interface ILinkRepository
    {
        Task<Link> CreateAsync(Link unit);
        Task<Link> UpdateAsync(Link unit);
        Task<Link> GetByIdAsync(Guid id);
        Task<IEnumerable<Link>> GetByReferenceIdAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
