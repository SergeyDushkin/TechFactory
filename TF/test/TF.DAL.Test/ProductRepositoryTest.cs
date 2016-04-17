using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business.WMS;

namespace TF.DAL.Test
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestMethod]
        public void ProdutServiceCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");

            IProductRepository service = new ProductRepository(context);

            var id = Guid.NewGuid();

            var record = new Product
            {
                Id = id,
                Key = "Key_" + id.ToString(),
                Name = "Name_" + id.ToString(),
                Type = "REGULAR"
            };

            service.Create(record);

            record.Key = "U_" + record.Key;
            record.Name = "U_" + record.Name;

            service.Update(record);

            var record2 = service.GetById(id);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.Key, record2.Key);
            Assert.AreEqual(record.Name, record2.Name);
            Assert.AreEqual(record.Type, record2.Type);

            service.Delete(record.Id);

            var record3 = service.GetById(id);

            Assert.IsNull(record3);
        }
    }
}
