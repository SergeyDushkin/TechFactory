using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Web.API.Test.NoodleService;
using System.Data.Services.Client;
using System.Linq;
using TF.Data.Business.WMS;

namespace TF.Web.API.Test
{
    [TestClass]
    public class OrderLineDetailEndpointTest
    {
        [TestMethod]
        public void OrderLineDetailCrudTest()
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

            var location = new Location
            {
                Key = "TestLocation" + Guid.NewGuid(),
                Name = "TestLocation",
                Type = "WAREHOUSE",
                UnitId = Guid.NewGuid()
            };

            context.AddToLocations(location);
            context.SaveChanges();
            var savedLocation = context.Locations.Where(l => l.Key == location.Key).Single();

            var orderLine = new OrderLine
            {
                Id = Guid.NewGuid(),
                BasePrice = .1f,
                BaseQty = .1f,
                Price = .1f,
                Priority = 1,
                Qty = .1f,
                Amount = 10,
                BaseAmount = 10
            };

            context.AddToOrderLines(orderLine);
            context.SaveChanges();
            var savedOrderLine = context.OrderLines.Where(ol => ol.Id == orderLine.Id).Single();

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
            var savedOrder = context.Orders.Where(o => o.Id == o.Id).Single();

            var orderLineDetail = new OrderLineDetail
            {
                BaseQty = .1f,
                Priority = 1,
                Qty = .1f,
                Number = "SO001",
                ItemId = savedItem.Id,
                LocationId = savedLocation.Id,
                OrderLineId = savedOrderLine.Id,
                UomId = savedUom.Id,
                OrderId = order.Id
            };

            context.AddToOrderLineDetails(orderLineDetail);

            var response = context.SaveChanges();

            foreach (ChangeOperationResponse change in response)
            {
                var descriptor = change.Descriptor as EntityDescriptor;
                var entity = descriptor.Entity as OrderLineDetail;

                entity.Number = "SO002";
                context.UpdateObject(entity);
                context.SaveChanges(SaveChangesOptions.ReplaceOnUpdate);

                var savedEntity = context.OrderLineDetails.Where(r => r.Id == entity.Id).Single();
                var referencedItem = context.OrderLineDetails.Where(r => r.Id == entity.Id).Select(r => r.Item).Single();
                var referencedLocation = context.OrderLineDetails.Where(r => r.Id == entity.Id).Select(r => r.Location).Single();
                var referencedOrderLine = context.OrderLineDetails.Where(r => r.Id == entity.Id).Select(r => r.OrderLine).Single();
                var referencedUom = context.OrderLineDetails.Where(r => r.Id == entity.Id).Select(r => r.Uom).Single();
                var referencedOrder = context.OrderLineDetails.Where(r => r.Id == entity.Id).Select(r => r.Order).Single();

                context.DeleteObject(savedItem);
                context.DeleteObject(savedLocation);
                context.DeleteObject(savedOrder);
                context.DeleteObject(savedOrderLine);
                context.DeleteObject(savedUom);
                context.DeleteObject(entity);
                var deleteResponses = context.SaveChanges();

                Assert.IsNotNull(savedEntity);
                Assert.AreEqual(referencedItem.Key,item.Key);
                Assert.AreEqual(referencedLocation.Key, location.Key);
                Assert.AreEqual(referencedOrderLine.Id, orderLine.Id);
                Assert.AreEqual(referencedUom.Key, referencedUom.Key);
                Assert.AreEqual(referencedOrder.Id, order.Id);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(response);
        }
    }
}
