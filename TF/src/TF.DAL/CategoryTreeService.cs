using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using TF.Data.Business;
using TF.DAL.Query;

namespace TF.DAL
{
    public partial class CategoryTreeService : ICategoryService
    {
        private readonly NoodleDbContext context;
        private readonly string _connectionString;

        public CategoryTreeService(NoodleDbContext context)
        {
            this.context = context;
            _connectionString = context.ConnectionString;
        }

        public IEnumerable<Category> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Category>(CategoryQuery.All());
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Category>(CategoryQuery.All());
            }
        }

        public Category GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Category>(CategoryQuery.ById(id)).SingleOrDefault();
            }
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<Category>(CategoryQuery.ById(id));
                return query.SingleOrDefault();
            }
        }

        public IEnumerable<Category> GetByParentId(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Category>(CategoryQuery.ByParentId(id));
            }
        }

        public async Task<IEnumerable<Category>> GetByParentIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Category>(CategoryQuery.ByParentId(id));
            }
        }
    }
}
