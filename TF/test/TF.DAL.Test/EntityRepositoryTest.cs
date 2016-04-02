using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Systems;

namespace TF.DAL.Test
{
    [TestClass]
    public class EntityRepositoryTest
    {
        [TestMethod]
        public void EntityRepositoryCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();

            IEntityRepository repository = new EntityRepository(context);

            var id = Guid.NewGuid();

            var record = new Entity
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
