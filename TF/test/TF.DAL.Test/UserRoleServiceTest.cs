using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Systems.Security;

namespace TF.DAL.Test
{
    [TestClass]
    public class UserRoleServiceTest
    {
        [TestMethod]
        public void UserRoleCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");

            IUserRoleRepository service = new UserRoleRepository(context);

            var ID_ = Guid.NewGuid();
            var USER_GUID_ = Guid.NewGuid();
            var ROLE_GUID_ = Guid.NewGuid();

            var record = new UserRole
            {
                Id = ID_,
                UserGuid = USER_GUID_,
                RoleGuid = ROLE_GUID_
            };

            service.Create(record);

            record.UserGuid = Guid.NewGuid();
            record.RoleGuid = Guid.NewGuid();

            service.Update(record);

            var record2 = service.GetById(ID_);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.RoleGuid, record2.RoleGuid);
            Assert.AreEqual(record.UserGuid, record2.UserGuid);

            service.Delete(record.Id);

            var record3 = service.GetById(ID_);

            Assert.IsNull(record3);
        }
    }
}
