using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Web.API.Test.NoodleService;
using System.Data.Services.Client;
using System.Linq;
using TF.Data.Systems.Security;

namespace TF.Web.API.Test
{
    [TestClass]
    public class UserEndpointTest
    {
        [TestMethod]
        public void UserCrudTest()
        {
            var context = new Container(new Uri("http://localhost:5588/odata/"));

            context.AddToUsers(new User
            {
                Key = "TestUser" + Guid.NewGuid(),
            });

            
            var response = context.SaveChanges();

            foreach (ChangeOperationResponse change in response)
            {
                var descriptor = change.Descriptor as EntityDescriptor;
                var entity = descriptor.Entity as User;

                entity.Key = "upd";

                context.UpdateObject(entity);
                context.SaveChanges(SaveChangesOptions.ReplaceOnUpdate);

                var savedEntity = context.Users.Where(r => r.Key == entity.Key).Single();

                context.DeleteObject(entity);
                var deleteResponses = context.SaveChanges();

                Assert.IsNotNull(savedEntity);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(response);
        }
    }
}
