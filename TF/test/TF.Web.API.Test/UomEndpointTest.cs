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
            var container = new Container(new Uri("http://localhost:5588/odata/"));

            container.AddToUoms(new Uom
            {
                Key = "TestUom",
                Name = "TestUom"
            });

            var responses = container.SaveChanges();

            foreach (var response in responses)
            {
                var changeResponse = (ChangeOperationResponse)response;
                var entityDescriptor = (EntityDescriptor)changeResponse.Descriptor;
                var entity = (Uom)entityDescriptor.Entity;

                entity.Name = "upd";

                container.UpdateObject(entity);
                container.SaveChanges(SaveChangesOptions.ReplaceOnUpdate);

                var savedEntity = container.Uoms.Where(r => r.Id == entity.Id).Single();

                container.DeleteObject(entity);
                var deleteResponses = container.SaveChanges();

                Assert.IsNotNull(savedEntity);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(responses);
        }
    }
}
