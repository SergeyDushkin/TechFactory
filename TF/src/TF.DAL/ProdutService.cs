using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TF.Data.Business.WMS;

namespace TF.DAL
{
    public class ProductService : IProductService
    {
        public void Delete(Guid id)
        {
            Product entity = null;
            using (var context = new NoodleDbContext())
            {
                entity = context.Products.Include("ChildProducts").SingleOrDefault(p => p.Id == id);
                if (entity != null)
                {
                    context.Products.Remove(entity);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public IEnumerable<Product> Get()
        {
            using (var context = new NoodleDbContext())
            {
                return context.Products.ToList();
            }
        }

        public Product GetById(Guid id)
        {
            using (var context = new NoodleDbContext())
            {
                return context.Products.Include("ChildProducts").SingleOrDefault(p => p.Id == id);
            }
        }

        public Product Update(Product product)
        {
            using (var context = new NoodleDbContext())
            {
                context.Products.Attach(product);
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();

                return product;
            }
        }

        public Product Create(Product product)
        {
            using (var context = new NoodleDbContext())
            {
                var result = context.Products.Add(product);
                context.SaveChanges();

                return context.Products.Add(result);
            }
        }
    }
}
