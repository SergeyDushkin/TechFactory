using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Web.API.Test.NoodleService;
using TF.Data.Business;
using System.Data.Services.Client;
using System.Linq;

namespace TF.Web.API.Test
{
    [TestClass]
    public class CategoriesEnpointTest
    {
        [TestMethod]
        public void CategoryCrudTest()
        {
            var container = new Container(new Uri("http://localhost:5588/odata/"));

            container.AddToCategories(new Category
            {
                Key = "TestCategory",
                Name = "TestCategory"
            });

            var responses = container.SaveChanges();

            foreach (var response in responses)
            {
                var changeResponse = (ChangeOperationResponse)response;
                var entityDescriptor = (EntityDescriptor)changeResponse.Descriptor;
                var entity = (Category)entityDescriptor.Entity;

                var entity2 = container.Categories.Where(r => r.Id == entity.Id).Single();

                container.DeleteObject(entity);
                var deleteResponses = container.SaveChanges();

                Assert.IsNotNull(entity2);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(responses);
        }
    }
}
