using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Web.API.Test.NoodleService;
using System.Data.Services.Client;
using System.Linq;
using TF.Data.Business.WMS;

namespace TF.Web.API.Test
{
    [TestClass]
    public class OrderEndpointTest
    {
        [TestMethod]
        public void OrderCrudTest()
        {
            var context = new Container(new Uri("http://localhost:5588/odata/"));

            var source = new Location
            {
                Key = "TestLocation" + Guid.NewGuid(),
                Name = "TestLocation",
                Type = "WAREHOUSE",
                UnitId = Guid.NewGuid()
            };

            context.AddToLocations(source);
            context.SaveChanges();
            var savedSource = context.Locations.Where(l => l.Key == source.Key).Single();

            var destination = new Location
            {
                Key = "TestLocation" + Guid.NewGuid(),
                Name = "TestLocation",
                Type = "WAREHOUSE",
                UnitId = Guid.NewGuid()
            };

            context.AddToLocations(destination);
            context.SaveChanges();
            var savedDestination = context.Locations.Where(l => l.Key == destination.Key).Single();

            var customer = new Unit()
            {
                Key = "TestUnit" + Guid.NewGuid(),
                Name = "TestUnit"
            };

            context.AddToUnits(customer);
            context.SaveChanges();
            var savedCustomer = context.Units.Where(c => c.Key == customer.Key).Single();

            var currency = new Currency()
            {
                Key = "TestCurrency" + Guid.NewGuid(),
                Name = "TestCurrency"
            };

            context.AddToCurrencies(currency);
            context.SaveChanges();
            var savedCurrency = context.Currencies.Where(cr => cr.Key == currency.Key).Single();

            var order = new Order
            {
                Amount = 10,
                BaseAmount = 10,
                CurrencyId = savedCurrency.Id,
                CustomerId = savedCustomer.Id,
                Date = new DateTime(2000, 1, 1),
                DestinationId = savedDestination.Id,
                DueDate = new DateTime(2001, 1, 1),
                LinesCount = 5,
                Number = "SO001",
                SourceId = savedSource.Id,
                Type = "SO"
            };

            context.AddToOrders(order);
            
            var response = context.SaveChanges();

            foreach (ChangeOperationResponse change in response)
            {
                var descriptor = change.Descriptor as EntityDescriptor;
                var entity = descriptor.Entity as Order;

                entity.Number = "SO002";
                context.UpdateObject(entity);
                context.SaveChanges(SaveChangesOptions.ReplaceOnUpdate);

                var orderLine1 = new OrderLine
                {
                    Id = Guid.NewGuid(),
                    BasePrice = .1f,
                    BaseQty = .1f,
                    Price = .1f,
                    Priority = 1,
                    Qty = .1f,
                    Amount = 10,
                    BaseAmount = 10,
                    OrderId = entity.Id
                };

                var orderLine2 = new OrderLine
                {
                    Id = Guid.NewGuid(),
                    BasePrice = .1f,
                    BaseQty = .1f,
                    Price = .1f,
                    Priority = 1,
                    Qty = .1f,
                    Amount = 10,
                    BaseAmount = 10,
                    OrderId = entity.Id
                };


                context.AddToOrderLines(orderLine1);
                context.AddToOrderLines(orderLine2);

                context.SaveChanges();


                var savedOrderLine1 = context.OrderLines.Where(ol => ol.Id == orderLine1.Id).Single();
                var savedOrderLine2 = context.OrderLines.Where(ol => ol.Id == orderLine2.Id).Single();

                var savedEntity = context.Orders.Where(r => r.Id == entity.Id).Single();
                var referencedDestination = context.Orders.Where(r => r.Id == entity.Id).Select(r => r.Destination).Single();
                var referencedSource = context.Orders.Where(r => r.Id == entity.Id).Select(r => r.Source).Single();
                var referencedCustomer = context.Orders.Where(r => r.Id == entity.Id).Select(r => r.Customer).Single();
                var referencedCurrency = context.Orders.Where(r => r.Id == entity.Id).Select(r => r.Currency).Single();
                var referencedLines = context.Orders.Where(r => r.Id == entity.Id).SelectMany(r => r.Lines).ToList();

                context.DeleteObject(savedOrderLine1);
                context.DeleteObject(savedOrderLine2);
                context.DeleteObject(referencedDestination);
                context.DeleteObject(referencedSource);
                context.DeleteObject(referencedCustomer);
                context.DeleteObject(referencedCurrency);
                context.DeleteObject(entity);
                var deleteResponses = context.SaveChanges();

                Assert.IsNotNull(savedEntity);
                Assert.AreEqual(referencedLines[0].Id, orderLine1.Id);
                Assert.AreEqual(referencedLines[1].Id, orderLine2.Id);
                Assert.AreEqual(savedEntity.Number, entity.Number);
                Assert.AreEqual(referencedDestination.Key,destination.Key);
                Assert.AreEqual(referencedSource.Key, source.Key);
                Assert.AreEqual(referencedCustomer.Key, customer.Key);
                Assert.AreEqual(referencedCurrency.Key, currency.Key);
                Assert.IsNotNull(deleteResponses);
            }

            Assert.IsNotNull(response);
        }
    }
}
