﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using TF.Data.Business;

namespace TF.DAL
{
    public partial class CategoryTreeService : ICategoryTreeService
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

        public IEnumerable<CategoryTree> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryTree>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public CategoryTree GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryTree> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryTree> GetByParentId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryTree>> GetByParentIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
