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
    public class OrderLineRepository : IOrderLineRepository
    {
        private readonly NoodleDbContext context;

        public OrderLineRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        public OrderLine GetById(Guid id)
        {
            return GetByIdAsync(id).Result;
        }

        public OrderLine Update(OrderLine OrderLine)
        {
            return UpdateAsync(OrderLine).Result;
        }

        public OrderLine Create(OrderLine OrderLine)
        {
            if (OrderLine.Id == Guid.Empty)
                OrderLine.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(OrderLineQuery.Insert(OrderLine));
                return OrderLine;
            }
        }

        public async Task<OrderLine> CreateAsync(OrderLine OrderLine)
        {
            if (OrderLine.Id == Guid.Empty)
                OrderLine.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(OrderLineQuery.Insert(OrderLine));
                return OrderLine;
            }
        }

        public async Task<OrderLine> UpdateAsync(OrderLine OrderLine)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(OrderLineQuery.Update(OrderLine));
                return OrderLine;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(OrderLineQuery.Delete(id));
            }
        }

        public async Task<OrderLine> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<OrderLine>(OrderLineQuery.ById(id));
                
                return query.SingleOrDefault();
            }
        }

        public IEnumerable<OrderLine> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IEnumerable<OrderLine>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<OrderLine>(OrderLineQuery.All());
            }
        }
    }
}
