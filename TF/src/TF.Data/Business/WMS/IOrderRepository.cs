using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business.WMS
{
    public interface IOrderRepository
    {
        Order Create(Order order);
        Task<Order> CreateAsync(Order order);

        Order Update(Order order);
        Task<Order> UpdateAsync(Order order);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        Order GetById(Guid id);
        Task<Order> GetByIdAsync(Guid id);

        IEnumerable<Order> GetAll();
        Task<IEnumerable<Order>> GetAllAsync();
    }
}
