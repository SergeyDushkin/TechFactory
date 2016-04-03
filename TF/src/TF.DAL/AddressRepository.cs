using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business;

namespace TF.DAL
{
    public class AddressRepository : IAddressRepository
    {
        private readonly NoodleDbContext context;

        public AddressRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        public Address GetById(Guid id)
        {
            return GetByIdAsync(id).Result;
        }

        public Address Update(Address Address)
        {
            return UpdateAsync(Address).Result;
        }

        public Address Create(Address Address)
        {
            if (Address.Id == Guid.Empty)
                Address.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(AddressQuery.Insert(Address));
                return Address;
            }
        }

        public async Task<Address> CreateAsync(Address Address)
        {
            if (Address.Id == Guid.Empty)
                Address.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(AddressQuery.Insert(Address));
                return Address;
            }
        }

        public async Task<Address> UpdateAsync(Address Address)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(AddressQuery.Update(Address));
                return Address;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(AddressQuery.Delete(id));
            }
        }

        public async Task<Address> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<Address>(AddressQuery.ById(id));
                
                return query.SingleOrDefault();
            }
        }

        public IEnumerable<Address> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Address>(AddressQuery.All());
            }
        }
    }
}
