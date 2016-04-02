using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Systems.Security;

namespace TF.DAL.Test
{
    [TestClass]
    public class RoleServiceTest
    {
        [TestMethod]
        public void RoleCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");

            IRoleRepository service = new RoleRepository(context);

            var ID_ = Guid.NewGuid();
            var KEY_ = Guid.NewGuid().ToString();
            var Name_ = Guid.NewGuid().ToString();

            var record = new Role
            {
                Id = ID_,
                Key = KEY_,
                Name = Name_
            };

            service.Create(record);

            record.Name = Guid.NewGuid().ToString();

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
