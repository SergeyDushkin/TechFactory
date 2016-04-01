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
            var context = new NoodleDbContext("PersonDb");

            IUserRepository service = new UserRepository(context);

            var ID_ = Guid.NewGuid();
            var KEY_ = Guid.NewGuid().ToString();

            var record = new USER
            {
                ID = ID_,
                KEY = KEY_,
                LAST_LOGIN = Convert.ToDateTime("2016-04-01 15:41:00")
             };

            service.Create(record);

            record.KEY = Guid.NewGuid().ToString();
            record.LAST_LOGIN = Convert.ToDateTime("2016-04-01 15:41:00");

            service.Update(record);

            var record2 = service.GetById(ID_);

            Assert.AreEqual(record.ID, record2.ID);
            Assert.AreEqual(record.KEY, record2.KEY);

            service.Delete(record.ID);

            var record3 = service.GetById(ID_);

            Assert.IsNull(record3);
        }
    }
}
