using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business.WMS;

namespace TF.DAL.Test
{
    [TestClass]
    public class OrderLineDetailRepositoryTest
    {
        [TestMethod]
        public void OrderLineDetailRepositoryCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();

            IOrderLineDetailRepository repository = new OrderLineDetailRepository(context);

            var id = Guid.NewGuid();

            var record = new OrderLineDetail
            {
                Id = id,
                BaseQty = .1f,
                Priority = 1,
                Qty = .1f,
                Number = "SO001"
            };

            repository.Create(record);

            record.BaseQty = .2f;
            record.Priority = 2;
            record.Qty = .2f;
            record.Number = "SO002";

            repository.Update(record);

            var updatedRecord = repository.GetById(id);

            Assert.AreEqual(record.Id, updatedRecord.Id);
            Assert.AreEqual(record.BaseQty, updatedRecord.BaseQty);
            Assert.AreEqual(record.Priority, updatedRecord.Priority);
            Assert.AreEqual(record.Qty, updatedRecord.Qty);
            Assert.AreEqual(record.Number, updatedRecord.Number);

            repository.Delete(record.Id);

            var deletedRecord = repository.GetById(id);

            Assert.IsNull(deletedRecord);
        }
    }
}
