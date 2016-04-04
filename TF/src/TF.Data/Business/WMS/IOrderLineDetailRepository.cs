using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business.WMS
{
    public interface IOrderLineDetailRepository
    {
        OrderLineDetail Create(OrderLineDetail OrderLineDetail);
        Task<OrderLineDetail> CreateAsync(OrderLineDetail OrderLineDetail);

        OrderLineDetail Update(OrderLineDetail OrderLineDetail);
        Task<OrderLineDetail> UpdateAsync(OrderLineDetail OrderLineDetail);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        OrderLineDetail GetById(Guid id);
        Task<OrderLineDetail> GetByIdAsync(Guid id);

        IEnumerable<OrderLineDetail> GetAll();
        Task<IEnumerable<OrderLineDetail>> GetAllAsync();
    }
}
