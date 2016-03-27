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

                entity.Name = "upd";

                container.UpdateObject(entity);
                container.SaveChanges(SaveChangesOptions.ReplaceOnUpdate);

                var price = new ProductPrice
                {
                    ProductId = entity.Id,
                    Price = 10
                };

                container.AddToProductPrices(price);
                container.SaveChanges();

                /// Get Product
                var entity2 = container.Products.Where(r => r.Id == entity.Id).Single();

                /// Get Related ProductPrice
                var entity3 = container.Products.Where(r => r.Id == entity.Id).Select(r => r.Price).Single();

                container.DeleteObject(entity);
                var deleteResponses = container.SaveChanges();

                Assert.IsNotNull(entity2);
                Assert.IsNotNull(entity3);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(responses);
        }

        /*
        public async Task UpdateProductAsync(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5588/odata/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PutAsJsonAsync("Products(Guid'" + product.Id + "')", product);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task AddProductPriceAsync(Guid productId, ProductPrice productPrice)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5588/odata/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Products(Guid'" + productId + "')/Price", productPrice);
                response.EnsureSuccessStatusCode();
            }
        }*/
    }
}
