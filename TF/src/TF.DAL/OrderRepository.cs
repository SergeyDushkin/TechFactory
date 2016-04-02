using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business;
using TF.Data.Business.WMS;

namespace TF.DAL
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NoodleDbContext context;

        public OrderRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        public Order GetById(Guid id)
        {
            return GetByIdAsync(id).Result;
        }

        public Order Update(Order order)
        {
            return UpdateAsync(order).Result;
        }

        public Order Create(Order order)
        {
            if (order.Id == Guid.Empty)
                order.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(OrderQuery.Insert(order));
                return order;
            }
        }

        public async Task<Order> CreateAsync(Order order)
        {
            if (order.Id == Guid.Empty)
                order.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(OrderQuery.Insert(order));
                return order;
            }
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(OrderQuery.Update(order));
                return order;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(OrderQuery.Delete(id));
            }
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<Order>(OrderQuery.ById(id));
                
                return query.SingleOrDefault();
            }
        }

        public IEnumerable<Order> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Order>(OrderQuery.All());
            }
        }
    }
}
