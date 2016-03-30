using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business;

namespace TF.DAL.Test
{
    [TestClass]
    public class UnitRepositoryTest
    {
        [TestMethod]
        public void UnitRepositoryTestCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();

            IUnitRepository repository = new UnitRepository(context);

            var id = Guid.NewGuid();

            var record = new Unit
            {
                Id = id,
                Key = "Key_" + id.ToString(),
                Name = "Name_" + id.ToString()
            };

            repository.Create(record);

            record.Key = "U_" + record.Key;
            record.Name = "U_" + record.Name;

            repository.Update(record);

            var record2 = repository.GetById(id);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.Key, record2.Key);
            Assert.AreEqual(record.Name, record2.Name);

            repository.Delete(record.Id);

            var record3 = repository.GetById(id);

            Assert.IsNull(record3);
        }
    }
}
