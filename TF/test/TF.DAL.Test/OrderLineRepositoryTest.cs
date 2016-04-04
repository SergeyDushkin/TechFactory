using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business.WMS;

namespace TF.DAL.Test
{
    [TestClass]
    public class OrderLineRepositoryTest
    {
        [TestMethod]
        public void OrderLineRepositoryCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();

            IOrderLineRepository repository = new OrderLineRepository(context);

            var id = Guid.NewGuid();

            var record = new OrderLine
            {
                Id = id,
                BasePrice = .1f,
                BaseQty = .1f,
                Price = .1f,
                Priority = 1,
                Qty = .1f,
                Amount = 10,
                BaseAmount = 10
            };

            repository.Create(record);

            record.BasePrice = .2f;
            record.BaseQty = .2f;
            record.Price = .2f;
            record.Priority = 2;
            record.Qty = .2f;
            record.Amount = 11;
            record.BaseAmount = 11;

            repository.Update(record);

            var updatedRecord = repository.GetById(id);

            Assert.AreEqual(record.Id, updatedRecord.Id);
            Assert.AreEqual(record.BasePrice, updatedRecord.BasePrice);
            Assert.AreEqual(record.BaseQty, updatedRecord.BaseQty);
            Assert.AreEqual(record.Price, updatedRecord.Price);
            Assert.AreEqual(record.Priority, updatedRecord.Priority);
            Assert.AreEqual(record.Qty, updatedRecord.Qty);
            Assert.AreEqual(record.Amount, updatedRecord.Amount);
            Assert.AreEqual(record.BaseAmount, updatedRecord.BaseAmount);

            repository.Delete(record.Id);

            var deletedRecord = repository.GetById(id);

            Assert.IsNull(deletedRecord);
        }
    }
}
