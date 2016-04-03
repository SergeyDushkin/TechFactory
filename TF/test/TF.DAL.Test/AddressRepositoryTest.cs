using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business;

namespace TF.DAL.Test
{
    [TestClass]
    public class AddressRepositoryTest
    {
        [TestMethod]
        public void AddressRepositoryCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();

            IAddressRepository repository = new AddressRepository(context);

            var id = Guid.NewGuid();

            var record = new Address
            {
                Id = id,
                City = "_City",
                Country = "_Contry",
                Elevation = 0.1f,
                Latitude = 0.1f,
                Line1 = "_Line1" + id.ToString(),
                Line2 = "_Line2" + id.ToString(),
                Longitude = 0.1f,
                Postalcode = "_Postalcode",
                Type = "_Type"
            };

            repository.Create(record);

            record.City = "U_" + record.City;
            record.Country = "U_" + record.Country;
            record.Line1 = "U_" + record.Line1;
            record.Line2 = "U_" + record.Line2;
            record.Postalcode = "U_" + record.Postalcode;
            record.Type = "U_" + record.Type;
            record.Elevation = 0.2f;
            record.Latitude = 0.2f;
            record.Longitude = 0.2f;

            repository.Update(record);

            var UpdatedRecord = repository.GetById(id);

            Assert.AreEqual(record.Id, UpdatedRecord.Id);
            Assert.AreEqual(record.City, UpdatedRecord.City);
            Assert.AreEqual(record.Country, UpdatedRecord.Country);
            Assert.AreEqual(record.Line1, UpdatedRecord.Line1);
            Assert.AreEqual(record.Line2, UpdatedRecord.Line2);
            Assert.AreEqual(record.Postalcode, UpdatedRecord.Postalcode);
            Assert.AreEqual(record.Type, UpdatedRecord.Type);
            Assert.AreEqual(record.Elevation, UpdatedRecord.Elevation);
            Assert.AreEqual(record.Latitude, UpdatedRecord.Latitude);
            Assert.AreEqual(record.Longitude, UpdatedRecord.Longitude);

            repository.Delete(record.Id);

            var deletedRecord = repository.GetById(id);

            Assert.IsNull(deletedRecord);
        }
    }
}
