using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business.WMS;

namespace TF.DAL.Test
{
    [TestClass]
    public class ProductCategoryServiceTest
    {
        [TestMethod]
        public void ProductCategoryServiceCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");

            IProductCategoryService service = new ProductCategoryService(context);

            var id = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();

            var record = new ProductCategory
            {
                Id = id,
                ProductId = productId,
                CategoryId = categoryId
            };

            service.Create(record);

            record.CategoryId = Guid.NewGuid();
            record.ProductId = Guid.NewGuid();

            service.Update(record);

            var record2 = service.GetById(id);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.CategoryId, record2.CategoryId);
            Assert.AreEqual(record.ProductId, record2.ProductId);

            service.Delete(record.Id);

            var record3 = service.GetById(id);

            Assert.IsNull(record3);
        }
    }
}
