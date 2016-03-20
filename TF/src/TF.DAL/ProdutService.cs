using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using TF.Data.Business.WMS;
using TF.DAL.Query;

namespace TF.DAL
{
    public class ProductService : IProductService
    {
        private readonly NoodleDbContext context;

        public ProductService(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(SelectProduct.Delete(id));
            }
        }

        public IEnumerable<Product> Get()
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Product>(SelectProduct.All());
            }
        }

        public Product GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Product>(SelectProduct.ById(id)).SingleOrDefault();
            }
        }

        public Product Update(Product product)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(SelectProduct.Update(product));
                return product;
            }
        }

        public Product Create(Product product)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(SelectProduct.Insert(product));
                return product;
            }
        }
    }
}
