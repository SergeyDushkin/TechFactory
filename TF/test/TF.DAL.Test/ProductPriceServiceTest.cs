using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business.WMS;

namespace TF.DAL.Test
{
    [TestClass]
    public class ProductPriceServiceTest
    {
        [TestMethod]
        public void ProductPriceServiceCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();

            IProductPriceService service = new ProductPriceService(context);

            var id = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var record = new ProductPrice
            {
                Id = id,
                ProductId = productId,
                Price = 10,
                CurrencyCode = "USD"
            };

            service.Create(record);

            record.Price = 20;

            service.Update(record);

            var record2 = service.GetById(id);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.ProductId, record2.ProductId);
            Assert.AreEqual(record.Price, record2.Price);
            Assert.AreEqual(record.CurrencyCode, record2.CurrencyCode);

            service.Delete(record.Id);

            var record3 = service.GetById(id);

            Assert.IsNull(record3);
        }
    }
}
