using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Web.API.Test.NoodleService;
using System.Data.Services.Client;
using System.Linq;

namespace TF.Web.API.Test
{
    [TestClass]
    public class LocationEndpointTest
    {
        [TestMethod]
        public void LocationCrudTest()
        {
            var context = new Container(new Uri("http://localhost:5588/odata/"));

            var unit = new Unit()
            {
                Key = "TestUnit",
                Name = "TestUnit"
            };

            context.AddToUnits(unit);

            context.SaveChanges();

            var savedUnit = context.Units.Where(u => u.Key == unit.Key).Single();


            var location = new Location
            {
                Key = "TestLocation",
                Name = "TestLocation",
                Type = "WAREHOUSE",
                UnitId = savedUnit.Id
            };

            context.AddToLocations(location);

            var response = context.SaveChanges();

            foreach (ChangeOperationResponse change in response)
            {
                var descriptor = change.Descriptor as EntityDescriptor;
                var entity = descriptor.Entity as Location;

                entity.Name = "upd";
                context.UpdateObject(entity);
                context.SaveChanges(SaveChangesOptions.ReplaceOnUpdate);

                var savedEntity = context.Locations.Where(r => r.Id == entity.Id).Single();
                var referencedUnit = context.Locations.Where(r => r.Id == entity.Id).Select(r => r.Unit).Single();

                context.DeleteObject(savedUnit);
                context.DeleteObject(entity);
                var deleteResponses = context.SaveChanges();

                Assert.IsNotNull(savedEntity);
                Assert.AreEqual(savedEntity.Unit.Key,unit.Key);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(response);
        }
    }
}
