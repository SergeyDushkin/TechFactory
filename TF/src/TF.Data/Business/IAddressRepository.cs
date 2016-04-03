using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface IAddressRepository
    {
        Address Create(Address address);
        Task<Address> CreateAsync(Address address);

        Address Update(Address Address);
        Task<Address> UpdateAsync(Address address);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        Address GetById(Guid id);
        Task<Address> GetByIdAsync(Guid id);

        IEnumerable<Address> GetAll();
        Task<IEnumerable<Address>> GetAllAsync();
    }
}
