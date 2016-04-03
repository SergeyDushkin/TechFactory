using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using TF.Data.Business.WMS;
using TF.DAL.Query;
using TF.Data.Business;

namespace TF.DAL
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly NoodleDbContext context;

        public ProductCategoryService(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(ProductCategoryQuery.Delete(id));
            }
        }

        public IEnumerable<ProductCategory> GetByCategoryId(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<ProductCategory>(ProductCategoryQuery.ByCategoryId(id));
            }
        }

        public IEnumerable<ProductCategory> GetByProductId(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<ProductCategory>(ProductCategoryQuery.ByProductId(id));
            }
        }

        public IEnumerable<Product> GetProductsByCategoryId(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Product>(ProductCategoryQuery.ProductsByCategoryId(id));
            }
        }

        public IEnumerable<Category> GetCategoriesByProductId(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Category>(ProductCategoryQuery.CategoriesByProductId(id));
            }
        }

        public ProductCategory GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<ProductCategory>(ProductCategoryQuery.ById(id)).SingleOrDefault();
            }
        }

        public ProductCategory Update(ProductCategory productCategory)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(ProductCategoryQuery.Update(productCategory));
                return productCategory;
            }
        }

        public ProductCategory Create(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("productCategory");

            if (productCategory.Id == Guid.Empty)
                productCategory.Id = Guid.NewGuid();

            if (productCategory.ProductId == Guid.Empty)
                throw new ArgumentNullException("ProductId");

            if (productCategory.CategoryId == Guid.Empty)
                throw new ArgumentNullException("CategoryId");

            using (var connection = context.CreateConnection())
            {
                connection.Execute(ProductCategoryQuery.Insert(productCategory));
                return productCategory;
            }
        }
    }
}
