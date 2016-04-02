using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business;

namespace TF.DAL
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly NoodleDbContext context;

        public CurrencyRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        public Currency GetById(Guid id)
        {
            return GetByIdAsync(id).Result;
        }

        public Currency Update(Currency currency)
        {
            return UpdateAsync(currency).Result;
        }

        public Currency Create(Currency currency)
        {
            if (currency.Id == Guid.Empty)
                currency.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(CurrencyQuery.Insert(currency));
                return currency;
            }
        }

        public async Task<Currency> CreateAsync(Currency currency)
        {
            if (currency.Id == Guid.Empty)
                currency.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(CurrencyQuery.Insert(currency));
                return currency;
            }
        }

        public async Task<Currency> UpdateAsync(Currency currency)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(CurrencyQuery.Update(currency));
                return currency;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(CurrencyQuery.Delete(id));
            }
        }

        public async Task<Currency> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<Currency>(CurrencyQuery.ById(id));
                
                return query.SingleOrDefault();
            }
        }

        public IEnumerable<Currency> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Currency>(CurrencyQuery.All());
            }
        }
    }
}
