using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Systems.Security;

namespace TF.DAL.Test
{
    [TestClass]
    public class UserIdentityServiceTest
    {
        [TestMethod]
        public void UserIdentityCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");

            IUserIdentityRepository service = new UserIdentityRepository(context);

            var ID_ = Guid.NewGuid();
            var PROVIDER_ = "tst";
            var KEY_ = Guid.NewGuid().ToString(); 

            var record = new UserIdentity
            {
                Id = ID_,
                Provider = PROVIDER_,
                Key = KEY_
             };

            service.Create(record);

            record.Key = Guid.NewGuid().ToString();

            service.Update(record);

            var record2 = service.GetById(ID_);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.Key, record2.Key);

            service.Delete(record.Id);

            var record3 = service.GetById(ID_);

            Assert.IsNull(record3);
        }
    }
}
