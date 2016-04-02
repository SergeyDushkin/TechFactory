using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface IUomRepository
    {
        Uom Create(Uom uom);
        Task<Uom> CreateAsync(Uom uom);

        Uom Update(Uom uom);
        Task<Uom> UpdateAsync(Uom uom);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        Uom GetById(Guid id);
        Task<Uom> GetByIdAsync(Guid id);

        IEnumerable<Uom> GetAll();
        Task<IEnumerable<Uom>> GetAllAsync();
    }
}
