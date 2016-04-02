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
            var context = new NoodleDbContext("NoodleDb");

            IPersonRepository service = new PersonRepository(context);

            var ID_ = Guid.NewGuid();
            var FIRSTNAME_ = Guid.NewGuid().ToString();
            var LASTNAME_ = Guid.NewGuid().ToString();
          

            var record = new Person
            {
                Id = ID_,
                Firstname = FIRSTNAME_,
                Lastname = LASTNAME_
            };

            service.Create(record);

            record.Lastname = Guid.NewGuid().ToString();
            record.Birthdate = Convert.ToDateTime("2016-04-01 15:41:00");

            service.Update(record);

            var record2 = service.GetById(ID_);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.Lastname, record2.Lastname);
            Assert.AreEqual(record.Firstname, record2.Firstname);
            Assert.AreEqual(record.Birthdate, record2.Birthdate);

            service.Delete(record.Id);

            var record3 = service.GetById(ID_);

            Assert.IsNull(record3);
        }
    }
}
