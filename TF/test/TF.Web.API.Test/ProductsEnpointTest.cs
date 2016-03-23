using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Web.API.Test.NoodleService;
using System.Data.Services.Client;
using System.Linq;

namespace TF.Web.API.Test
{
    [TestClass]
    public class ProductsEnpointTest
    {
        [TestMethod]
        public void ProductCrudTest()
        {
            var container = new Container(new Uri("http://localhost:5588/odata/"));
            
            container.AddToProducts(new Product
            {
                Key = "TestProduct",
                Name = "TestProduct",
                Type = "REGULAR"
            });

            var responses = container.SaveChanges();
            
            foreach (var response in responses)
            {
                var changeResponse = (ChangeOperationResponse)response;
                var entityDescriptor = (EntityDescriptor)changeResponse.Descriptor;
                var entity = (Product)entityDescriptor.Entity;

                var price = new ProductPrice
                {
                    ProductId = entity.Id,
                    Price = 10
                };

                container.AddToProductPrices(price);
                container.SaveChanges();

                //entity.Price = new ProductPrice() { Price = 10 };
                //entity.Name = "upd";
                //container.UpdateObject(entity);
                //container.SaveChanges();

                var entity2 = container.Products.Where(r => r.Id == entity.Id).Single();

                container.DeleteObject(entity);
                var deleteResponses = container.SaveChanges();

                Assert.IsNotNull(entity2);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(responses);
        }
    }
}
