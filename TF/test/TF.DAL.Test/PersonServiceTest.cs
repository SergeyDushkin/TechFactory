using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business;

namespace TF.DAL.Test
{
    [TestClass]
    public class PersonServiceTest
    {
        [TestMethod]
        public void PersonCRUDTest()
        {
            var context = new NoodleDbContext("PersonDb");

            IPersonRepository service = new PersonRepository(context);

            var ID_ = Guid.NewGuid();
            var FIRSTNAME_ = Guid.NewGuid().ToString();
            var LASTNAME_ = Guid.NewGuid().ToString();
          

            var record = new PERSON
            {
                ID = ID_,
                FIRSTNAME = FIRSTNAME_,
                LASTNAME = LASTNAME_
            };

            service.Create(record);

            record.LASTNAME = Guid.NewGuid().ToString();
            record.BIRTHDATE = Convert.ToDateTime("2016-04-01 15:41:00");

            service.Update(record);

            var record2 = service.GetById(ID_);

            Assert.AreEqual(record.ID, record2.ID);
            Assert.AreEqual(record.LASTNAME, record2.LASTNAME);
            Assert.AreEqual(record.FIRSTNAME, record2.FIRSTNAME);
            Assert.AreEqual(record.BIRTHDATE, record2.BIRTHDATE);

            service.Delete(record.ID);

            var record3 = service.GetById(ID_);

            Assert.IsNull(record3);
        }
    }
}
