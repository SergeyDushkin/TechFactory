using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business;

namespace TF.DAL.Test
{
    [TestClass]
    public class ContactDetailServiceTest
    {
        [TestMethod]
        public void ContactDetailCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");

            IContactDetailRepository service = new ContactDetailRepository(context);

            var ID_ = Guid.NewGuid();
            var CONTACT_GUID_ = Guid.NewGuid();
            var PRIORITY_ = Int16.MaxValue;
            var TYPE_ = "ТИП";
            var VALUE_ = "VAL";
            var VERIFIED_ = false;
            var ALLOW_ = false;

            var record = new ContactDetail
            {
                Id = ID_,
                ContactGuid = CONTACT_GUID_,
                Priority = PRIORITY_,
                Type = TYPE_,
                Value = VALUE_,
                Verified = VERIFIED_,
                Allow = ALLOW_

            };

            service.Create(record);

            record.ContactGuid = Guid.NewGuid();
            record.Type = "ТИП1";
            record.Value = "VAL1";
            record.Verified = true;
            record.Allow = true;

            service.Update(record);

            var record2 = service.GetById(ID_);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.ContactGuid, record2.ContactGuid);
            Assert.AreEqual(record.Type, record2.Type);
            Assert.AreEqual(record.Value, record2.Value);
            Assert.AreEqual(record.Verified, record2.Verified);
            Assert.AreEqual(record.Allow, record2.Allow);

            service.Delete(record.Id);

            var record3 = service.GetById(ID_);

            Assert.IsNull(record3);
        }
    }
}
