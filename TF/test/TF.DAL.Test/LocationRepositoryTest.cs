using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business;

namespace TF.DAL.Test
{
    [TestClass]
    public class LocationRepositoryTest
    {
        [TestMethod]
        public void LocationRepositoryCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();

            ILocationRepository repository = new LocationRepository(context);

            var id = Guid.NewGuid();

            var record = new Location
            {
                Id = id,
                UnitId = Guid.Empty,
                Type = "STORE",
                Key = "Key_" + id.ToString(),
                Name = "Name_" + id.ToString()
            };

            repository.Create(record);

            record.Key = "U_" + record.Key;
            record.Name = "U_" + record.Name;
            record.Type = "HOME";

            repository.Update(record);

            var updatedRecord = repository.GetById(id);

            Assert.AreEqual(record.Id, updatedRecord.Id);
            Assert.AreEqual(record.Key, updatedRecord.Key);
            Assert.AreEqual(record.Name, updatedRecord.Name);
            Assert.AreEqual(record.Type, updatedRecord.Type);

            repository.Delete(record.Id);

            var deletedRecord = repository.GetById(id);

            Assert.IsNull(deletedRecord);
        }
    }
}
