using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Web.API.Test.NoodleService;
using System.Data.Services.Client;
using System.Linq;

namespace TF.Web.API.Test
{
    [TestClass]
    public class CurrencyEndpointTest
    {
        [TestMethod]
        public void CurrencyCrudTest()
        {
            var context = new Container(new Uri("http://localhost:5588/odata/"));

            context.AddToCurrencies(new Currency
            {
                Key = "TestCurrency" + Guid.NewGuid(),
                Name = "TestCurrency"
            });

            
            var response = context.SaveChanges();

            foreach (ChangeOperationResponse change in response)
            {
                var descriptor = change.Descriptor as EntityDescriptor;
                var entity = descriptor.Entity as Currency;

                entity.Name = "upd";

                context.UpdateObject(entity);
                context.SaveChanges(SaveChangesOptions.ReplaceOnUpdate);

                var savedEntity = context.Currencies.Where(r => r.Key == entity.Key).Single();

                //context.DeleteObject(entity);
                var deleteResponses = context.SaveChanges();

                Assert.IsNotNull(savedEntity);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(response);
        }
    }
}
