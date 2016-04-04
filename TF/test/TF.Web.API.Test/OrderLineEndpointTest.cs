using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Web.API.Test.NoodleService;
using System.Data.Services.Client;
using System.Linq;
using TF.Data.Business.WMS;

namespace TF.Web.API.Test
{
    [TestClass]
    public class OrderLineEndpointTest
    {
        [TestMethod]
        public void OrderLineCrudTest()
        {
            var context = new Container(new Uri("http://localhost:5588/odata/"));

            var item = new Product()
            {
                Key = "TestProduct" + Guid.NewGuid(),
                Name = "TestProduct",
                Type = "REGULAR"
            };

            context.AddToProducts(item);
            context.SaveChanges();
            var savedItem = context.Products.Where(u => u.Key == item.Key).Single();

            var uom = new Uom
            {
                Key = "TestUom" + Guid.NewGuid(),
                Name = "TestUom"
            };

            context.AddToUoms(uom);
            context.SaveChanges();
            var savedUom = context.Uoms.Where(u => u.Key == uom.Key).Single();

            var order = new Order
            {
                Id = Guid.NewGuid(),
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
                Type = "SO"
            };

            context.AddToOrders(order);
            context.SaveChanges();
            var savedOrder = context.Orders.Where(o => o.Id == order.Id).Single();

            var orderLine = new OrderLine
            {
                BasePrice = .1f,
                BaseQty = .1f,
                Price = .1f,
                Priority = 1,
                Qty = .1f,
                Amount = 10,
                BaseAmount = 10,
                ItemId = savedItem.Id,
                OrderId = savedOrder.Id,
                UomId = savedUom.Id
            };

            context.AddToOrderLines(orderLine);

            var response = context.SaveChanges();

            foreach (ChangeOperationResponse change in response)
            {
                var descriptor = change.Descriptor as EntityDescriptor;
                var entity = descriptor.Entity as OrderLine;

                entity.BasePrice = .2f;
                context.UpdateObject(entity);
                context.SaveChanges(SaveChangesOptions.ReplaceOnUpdate);

                var savedEntity = context.OrderLines.Where(r => r.Id == entity.Id).Single();
                var referencedItem = context.OrderLines.Where(r => r.Id == entity.Id).Select(r => r.Item).Single();
                var referencedUom = context.OrderLines.Where(r => r.Id == entity.Id).Select(r => r.Uom).Single();
                var referencedOrder = context.OrderLines.Where(r => r.Id == entity.Id).Select(r => r.Order).Single();

                context.DeleteObject(savedItem);
                context.DeleteObject(savedOrder);
                context.DeleteObject(savedUom);
                context.DeleteObject(entity);
                var deleteResponses = context.SaveChanges();

                Assert.IsNotNull(savedEntity);
                Assert.AreEqual(entity.BasePrice, savedEntity.BasePrice);
                Assert.AreEqual(referencedItem.Key,item.Key);
                Assert.AreEqual(referencedUom.Key, referencedUom.Key);
                Assert.AreEqual(referencedOrder.Id, order.Id);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(response);
        }
    }
}
