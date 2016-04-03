using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business;

namespace TF.DAL.Test
{
    [TestClass]
    public class ContactServiceTest
    {
        [TestMethod]
        public void ContactCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");

            IContactRepository service = new ContactRepository(context);

            var ID_ = Guid.NewGuid();
            var RECORD_GUID_ = Guid.NewGuid();
            var TYPE_ = "ТИП";

            var record = new Contact
            {
                Id = ID_,
                RecordGuid = RECORD_GUID_,
                Type = TYPE_
            };

            service.Create(record);

            record.RecordGuid = Guid.NewGuid();
            record.Type = "ТИП1";

            service.Update(record);

            var record2 = service.GetById(ID_);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.RecordGuid, record2.RecordGuid);
            Assert.AreEqual(record.Type, record2.Type);

            service.Delete(record.Id);

            var record3 = service.GetById(ID_);

            Assert.IsNull(record3);
        }
    }
}
