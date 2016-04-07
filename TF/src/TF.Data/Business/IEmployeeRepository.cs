using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface IEmployeeRepository
    {
        Employee Create(Employee employee);
        Task<Employee> CreateAsync(Employee employee);

        Employee Update(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);

        Employee GetById(Guid id);
        Task<Employee> GetByIdAsync(Guid id);

        IEnumerable<Employee> GetByUnitId(Guid id);
        Task<IEnumerable<Employee>> GetByUnitIdAsync(Guid id);

        IEnumerable<Employee> GetByPersonId(Guid id);
        Task<IEnumerable<Employee>> GetByPersonIdAsync(Guid id);

        IEnumerable<Employee> GetAll();
        Task<IEnumerable<Employee>> GetAllAsync();
    }
}
