using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business;

namespace TF.DAL
{
    public class LocationRepository : ILocationRepository
    {
        private readonly NoodleDbContext context;

        public LocationRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        public Location GetById(Guid id)
        {
            return GetByIdAsync(id).Result;
        }

        public Location Update(Location location)
        {
            return UpdateAsync(location).Result;
        }

        public Location Create(Location location)
        {
            if (location.Id == Guid.Empty)
                location.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(LocationQuery.Insert(location));
                return location;
            }
        }

        public async Task<Location> CreateAsync(Location location)
        {
            if (location.Id == Guid.Empty)
                location.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(LocationQuery.Insert(location));
                return location;
            }
        }

        public async Task<Location> UpdateAsync(Location location)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(LocationQuery.Update(location));
                return location;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(LocationQuery.Delete(id));
            }
        }

        public async Task<Location> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<Location>(LocationQuery.ById(id));
                
                return query.SingleOrDefault();
            }
        }

        public IEnumerable<Location> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Location>(LocationQuery.All());
            }
        }
    }
}
