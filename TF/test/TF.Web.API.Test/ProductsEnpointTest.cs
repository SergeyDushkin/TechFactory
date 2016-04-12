using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TF.Data.Business.WMS;
using Default;
using Microsoft.OData.Client;

namespace TF.Web.API.Test
{
    [TestClass]
    public class ProductsEnpointTest
    {
        [TestMethod]
        public void ProductCrudTest()
        {
            var container = new Container(new Uri("http://localhost:5588/odata/"));

            var product = new Product
            {
                Key = "TestProduct",
                Name = "TestProduct",
                Type = "REGULAR"
            };

            container.AddToProducts(product);

            var responses = container.SaveChanges();

            product.Name = "upd";

            container.UpdateObject(product);
            container.SaveChanges(SaveChangesOptions.ReplaceOnUpdate);

            var product1 = container.Products.ByKey(product.Id).GetValue();

            product1.Price = new ProductPrice
            {
                Price = 10
            };
            
            container.UpdateObject(product1);
            container.SaveChanges();

            var price = new ProductPrice
            {
                ProductId = product.Id,
                Price = 10
            };

            container.AddToProductPrices(price);
            container.SaveChanges();

            var category = new ProductCategory
            {
                ProductId = product.Id,
                CategoryId = Guid.Empty
            };

            container.AddToProductCategories(category);
            container.SaveChanges();

            /// Get Product
            var entity2 = container.Products.Where(r => r.Id == product.Id).Single();

            /// Get Related ProductPrice
            var entity3 = container.Products.Where(r => r.Id == product.Id).Select(r => r.Price).Single();

            /// Get Related ProductCategory
            var entity4 = container.Products.Where(r => r.Id == product.Id).SelectMany(r => r.Categories).ToList();

            container.DeleteObject(product);
            var deleteResponses = container.SaveChanges();

            Assert.IsNotNull(entity2);
            Assert.IsNotNull(entity3);
            Assert.IsNotNull(entity4);
            Assert.IsNotNull(deleteResponses);

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
