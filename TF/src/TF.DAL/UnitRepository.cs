using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business;

namespace TF.DAL
{
    public class UnitRepository : IUnitRepository
    {
        private readonly NoodleDbContext context;

        public UnitRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        public Unit GetById(Guid id)
        {
            return GetByIdAsync(id).Result;
        }

        public IEnumerable<Unit> GetByParentId(Guid id)
        {
            return GetByParentIdAsync(id).Result;
        }

        public Unit Update(Unit unit)
        {
            return UpdateAsync(unit).Result;
        }

        public Unit Create(Unit unit)
        {
            if (unit.Id == Guid.Empty)
                unit.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(UnitQuery.Insert(unit));
                return unit;
            }
        }

        public async Task<Unit> CreateAsync(Unit unit)
        {
            if (unit.Id == Guid.Empty)
                unit.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(UnitQuery.Insert(unit));
                return unit;
            }
        }

        public async Task<Unit> UpdateAsync(Unit unit)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(UnitQuery.Update(unit));
                return unit;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(UnitQuery.Delete(id));
            }
        }

        public async Task<IEnumerable<Unit>> GetByParentIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Unit>(UnitQuery.ByParentId(id));
            }
        }

        public async Task<Unit> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<Unit>(UnitQuery.ById(id));
                
                return query.SingleOrDefault();
            }
        }
    }
}
