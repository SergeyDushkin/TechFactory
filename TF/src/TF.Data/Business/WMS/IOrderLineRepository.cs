using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business.WMS
{
    public interface IOrderLineRepository
    {
        OrderLine Create(OrderLine OrderLine);
        Task<OrderLine> CreateAsync(OrderLine OrderLine);

        OrderLine Update(OrderLine OrderLine);
        Task<OrderLine> UpdateAsync(OrderLine OrderLine);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        OrderLine GetById(Guid id);
        Task<OrderLine> GetByIdAsync(Guid id);

        IEnumerable<OrderLine> GetAll();
        Task<IEnumerable<OrderLine>> GetAllAsync();
    }
}
