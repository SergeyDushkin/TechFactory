using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Systems.Security;

namespace TF.DAL.Test
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void UserCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");

            IUserRepository service = new UserRepository(context);

            var ID_ = Guid.NewGuid();
            var KEY_ = Guid.NewGuid().ToString();

            var record = new User
            {
                Id = ID_,
                Key = KEY_,
                LastLogin = Convert.ToDateTime("2016-04-01 15:41:00")
             };

            service.Create(record);

            record.Key = Guid.NewGuid().ToString();
            record.LastLogin = Convert.ToDateTime("2016-04-02 15:41:00");
            record.LoginAttemptCount = 1;

           service.Update(record);

            var record2 = service.GetById(ID_);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.Key, record2.Key);
            Assert.AreEqual(record.LastLogin, record2.LastLogin);
            Assert.AreEqual(record.LoginAttemptCount, record2.LoginAttemptCount);

            service.Delete(record.Id);

            var record3 = service.GetById(ID_);

            Assert.IsNull(record3);
        }
    }
}
