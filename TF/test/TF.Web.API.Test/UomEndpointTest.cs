using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Web.API.Test.NoodleService;
using System.Data.Services.Client;
using System.Linq;

namespace TF.Web.API.Test
{
    [TestClass]
    public class UomEndpointTest
    {
        [TestMethod]
        public void UomCrudTest()
        {
            var context = new Container(new Uri("http://localhost:5588/odata/"));

            context.AddToUoms(new Uom
            {
                Key = "TestUom",
                Name = "TestUom"
            });

            var response = context.SaveChanges();

            foreach (ChangeOperationResponse change in response)
            {
                var descriptor = change.Descriptor as EntityDescriptor;
                var entity = descriptor.Entity as Uom;

                entity.Name = "upd";

                context.UpdateObject(entity);
                context.SaveChanges(SaveChangesOptions.ReplaceOnUpdate);

                var savedEntity = context.Uoms.Where(r => r.Id == entity.Id).Single();

                //context.DeleteObject(entity);
                var deleteResponses = context.SaveChanges();

                Assert.IsNotNull(savedEntity);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(response);
        }
    }
}
