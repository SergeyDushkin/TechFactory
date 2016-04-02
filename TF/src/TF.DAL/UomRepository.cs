using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business;

namespace TF.DAL
{
    public class UomRepository : IUomRepository
    {
        private readonly NoodleDbContext context;

        public UomRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        public Uom GetById(Guid id)
        {
            return GetByIdAsync(id).Result;
        }

        public Uom Update(Uom uom)
        {
            return UpdateAsync(uom).Result;
        }

        public Uom Create(Uom uom)
        {
            if (uom.Id == Guid.Empty)
                uom.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(UomQuery.Insert(uom));
                return uom;
            }
        }

        public async Task<Uom> CreateAsync(Uom uom)
        {
            if (uom.Id == Guid.Empty)
                uom.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(UomQuery.Insert(uom));
                return uom;
            }
        }

        public async Task<Uom> UpdateAsync(Uom uom)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(UomQuery.Update(uom));
                return uom;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(UomQuery.Delete(id));
            }
        }

        public async Task<Uom> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<Uom>(UomQuery.ById(id));
                
                return query.SingleOrDefault();
            }
        }

        public IEnumerable<Uom> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IEnumerable<Uom>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Uom>(UomQuery.All());
            }
        }
    }
}
