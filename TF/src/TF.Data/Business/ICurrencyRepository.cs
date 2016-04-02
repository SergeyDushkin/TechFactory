using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface ICurrencyRepository
    {
        Currency Create(Currency currency);
        Task<Currency> CreateAsync(Currency currency);

        Currency Update(Currency currency);
        Task<Currency> UpdateAsync(Currency currency);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        Currency GetById(Guid id);
        Task<Currency> GetByIdAsync(Guid id);

        IEnumerable<Currency> GetAll();
        Task<IEnumerable<Currency>> GetAllAsync();
    }
}
