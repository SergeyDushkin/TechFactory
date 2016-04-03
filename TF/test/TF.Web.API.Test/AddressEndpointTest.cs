using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Web.API.Test.NoodleService;
using System.Data.Services.Client;
using System.Linq;

namespace TF.Web.API.Test
{
    [TestClass]
    public class AddressEndpointTest
    {
        [TestMethod]
        public void AddressCrudTest()
        {
            var context = new Container(new Uri("http://localhost:5588/odata/"));

            var id = Guid.NewGuid();

            var record = new Address
            {
                Id = id,
                City = "_City",
                Country = "_Contry",
                Elevation = 0.1f,
                Latitude = 0.1f,
                Line1 = "_Line1" + id.ToString(),
                Line2 = "_Line2" + id.ToString(),
                Longitude = 0.1f,
                Postalcode = "_Postalcode",
                Type = "_Type"
            };

            var response = context.SaveChanges();

            foreach (ChangeOperationResponse change in response)
            {
                var descriptor = change.Descriptor as EntityDescriptor;
                var entity = descriptor.Entity as Address;

                entity.City = "U_" + entity.City;
                entity.Country = "U_" + entity.Country;
                entity.Line1 = "U_" + entity.Line1;
                entity.Line2 = "U_" + entity.Line2;
                entity.Postalcode = "U_" + entity.Postalcode;
                entity.Type = "U_" + entity.Type;
                entity.Elevation = 0.2f;
                entity.Latitude = 0.2f;
                entity.Longitude = 0.2f;

                context.UpdateObject(entity);
                context.SaveChanges(SaveChangesOptions.ReplaceOnUpdate);

                var savedEntity = context.Addresses.Where(r => r.Id == entity.Id).Single();

                //context.DeleteObject(entity);
                var deleteResponses = context.SaveChanges();

                Assert.IsNotNull(savedEntity);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(response);
        }
    }
}
