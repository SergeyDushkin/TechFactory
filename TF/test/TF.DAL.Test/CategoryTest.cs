using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business;

namespace TF.DAL.Test
{
    [TestClass]
    public class CategoryTest
    {
        [TestMethod]
        public void CategoryCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");

            ICategoryService service = new CategoryTreeService(context);

            var id = Guid.NewGuid();

            var record = new Category
            {
                Id = id,
                Key = "Key_" + id.ToString(),
                Name = "Name_" + id.ToString()
            };

            service.Create(record);

            record.Key = "U_" + record.Key;
            record.Name = "U_" + record.Name;

            service.Update(record);

            var record2 = service.GetById(id);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.Key, record2.Key);
            Assert.AreEqual(record.Name, record2.Name);

            service.Delete(record.Id);

            var record3 = service.GetById(id);

            Assert.IsNull(record3);
        }
    }
}
