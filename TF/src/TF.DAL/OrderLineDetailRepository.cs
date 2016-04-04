using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business.WMS;

namespace TF.DAL
{
    public class OrderLineDetailRepository : IOrderLineDetailRepository
    {
        private readonly NoodleDbContext context;

        public OrderLineDetailRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        public OrderLineDetail GetById(Guid id)
        {
            return GetByIdAsync(id).Result;
        }

        public OrderLineDetail Update(OrderLineDetail OrderLineDetail)
        {
            return UpdateAsync(OrderLineDetail).Result;
        }

        public OrderLineDetail Create(OrderLineDetail OrderLineDetail)
        {
            if (OrderLineDetail.Id == Guid.Empty)
                OrderLineDetail.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(OrderLineDetailQuery.Insert(OrderLineDetail));
                return OrderLineDetail;
            }
        }

        public async Task<OrderLineDetail> CreateAsync(OrderLineDetail OrderLineDetail)
        {
            if (OrderLineDetail.Id == Guid.Empty)
                OrderLineDetail.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(OrderLineDetailQuery.Insert(OrderLineDetail));
                return OrderLineDetail;
            }
        }

        public async Task<OrderLineDetail> UpdateAsync(OrderLineDetail OrderLineDetail)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(OrderLineDetailQuery.Update(OrderLineDetail));
                return OrderLineDetail;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(OrderLineDetailQuery.Delete(id));
            }
        }

        public async Task<OrderLineDetail> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<OrderLineDetail>(OrderLineDetailQuery.ById(id));
                
                return query.SingleOrDefault();
            }
        }

        public IEnumerable<OrderLineDetail> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IEnumerable<OrderLineDetail>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<OrderLineDetail>(OrderLineDetailQuery.All());
            }
        }
    }
}
