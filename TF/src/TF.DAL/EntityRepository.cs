using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.Data.Systems;
using TF.DAL.Query;

namespace TF.DAL
{
    public class EntityRepository : IEntityRepository
    {
        private readonly NoodleDbContext context;

        public EntityRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        public Entity GetById(Guid id)
        {
            return GetByIdAsync(id).Result;
        }

        public IEnumerable<Entity> GetByParentId(Guid id)
        {
            return GetByParentIdAsync(id).Result;
        }

        public Entity Update(Entity entity)
        {
            return UpdateAsync(entity).Result;
        }

        public Entity Create(Entity entity)
        {
            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(EntityQuery.Insert(entity));
                return entity;
            }
        }

        public async Task<Entity> CreateAsync(Entity entity)
        {
            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(EntityQuery.Insert(entity));
                return entity;
            }
        }

        public async Task<Entity> UpdateAsync(Entity entity)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(EntityQuery.Update(entity));
                return entity;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(EntityQuery.Delete(id));
            }
        }

        public async Task<IEnumerable<Entity>> GetByParentIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Entity>(EntityQuery.ByParentId(id));
            }
        }

        public async Task<Entity> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<Entity>(EntityQuery.ById(id));
                
                return query.SingleOrDefault();
            }
        }

        public IEnumerable<Entity> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Entity>(EntityQuery.All());
            }
        }
    }
}
