using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business.WMS;

namespace TF.DAL.Test
{
    [TestClass]
    public class OrderRepositoryTest
    {
        [TestMethod]
        public void OrderRepositoryCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();

            IOrderRepository repository = new OrderRepository(context);

            var id = Guid.NewGuid();

            var record = new Order
            {
                Id = id,
                Amount = 10,
                BaseAmount = 10,
                CurrencyId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Date = new DateTime(2000, 1, 1),
                DestinationId = Guid.NewGuid(),
                DueDate = new DateTime(2001, 1, 1),
                LinesCount = 5,
                Number = "SO001",
                SourceId = Guid.NewGuid(),
                Type = "SO",
                StatusCode = "DRAFT"
            };

            repository.Create(record);

            record.Amount = 15;
            record.Number = "SO002";

            repository.Update(record);

            var record2 = repository.GetById(id);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.Amount, record2.Amount);
            Assert.AreEqual(record.BaseAmount, record2.BaseAmount);
            Assert.AreEqual(record.CurrencyId, record2.CurrencyId);
            Assert.AreEqual(record.CustomerId, record2.CustomerId);
            Assert.AreEqual(record.Date, record2.Date);
            Assert.AreEqual(record.DestinationId, record2.DestinationId);
            Assert.AreEqual(record.DueDate, record2.DueDate);
            Assert.AreEqual(record.LinesCount, record2.LinesCount);
            Assert.AreEqual(record.Number, record2.Number);
            Assert.AreEqual(record.SourceId, record2.SourceId);
            Assert.AreEqual(record.Type, record2.Type);
            Assert.AreEqual(record.StatusCode, record2.StatusCode);

            repository.Delete(record.Id);

            var record3 = repository.GetById(id);

            Assert.IsNull(record3);
        }
    }
}
