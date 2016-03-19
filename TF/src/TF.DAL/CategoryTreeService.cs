using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using TF.Data.Business;

namespace TF.DAL
{
    public partial class CategoryTreeService : ICategoryService
    {
        private readonly string _connectionString;

        public CategoryTreeService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CategoryTreeDb"].ConnectionString;
        }

        public CategoryTreeService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Category GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetByParentId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetByParentIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
