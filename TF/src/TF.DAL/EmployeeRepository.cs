using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business;

namespace TF.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly NoodleDbContext context;

        public EmployeeRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        public Employee GetById(Guid id)
        {
            return GetByIdAsync(id).Result;
        }

        public Employee Update(Employee employee)
        {
            return UpdateAsync(employee).Result;
        }

        public Employee Create(Employee employee)
        {
            return CreateAsync(employee).Result;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            if (employee.Id == Guid.Empty)
                employee.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(EmployeeQuery.Insert(employee));
                return employee;
            }
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(EmployeeQuery.Update(employee));
                return employee;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(EmployeeQuery.Delete(id));
            }
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<Employee>(EmployeeQuery.ById(id));
                
                return query.SingleOrDefault();
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Employee>(EmployeeQuery.All());
            }
        }


        public IEnumerable<Employee> GetByUnitId(Guid id)
        {
            return GetByUnitIdAsync(id).Result;
        }

        public async Task<IEnumerable<Employee>> GetByUnitIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Employee>(EmployeeQuery.ByUnitId(id));
            }
        }

        public IEnumerable<Employee> GetByPersonId(Guid id)
        {
            return GetByPersonIdAsync(id).Result;
        }

        public async Task<IEnumerable<Employee>> GetByPersonIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Employee>(EmployeeQuery.ByPersonId(id));
            }
        }
    }
}
