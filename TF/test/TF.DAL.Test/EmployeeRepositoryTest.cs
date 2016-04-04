using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business;

namespace TF.DAL.Test
{
    [TestClass]
    public class EmployeeRepositoryTest
    {
        [TestMethod]
        public void EmployeeRepositoryCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();

            IEmployeeRepository repository = new EmployeeRepository(context);

            var id = Guid.NewGuid();

            var record = new Employee
            {
                Id = id,
                UnitId = Guid.NewGuid(),
                PersonId = Guid.NewGuid()
            };

            repository.Create(record);

            record.UnitId = Guid.NewGuid();
            record.PersonId = Guid.NewGuid();

            repository.Update(record);

            var updatedRecord = repository.GetById(id);

            Assert.AreEqual(record.Id, updatedRecord.Id);
            Assert.AreEqual(record.PersonId, updatedRecord.PersonId);
            Assert.AreEqual(record.UnitId, updatedRecord.UnitId);

            var employeesByPersonId = repository.GetByPersonId(record.PersonId);
            var employeesByUnitId = repository.GetByUnitId(record.UnitId);

            Assert.IsNotNull(employeesByPersonId);
            Assert.IsNotNull(employeesByUnitId);

            repository.Delete(record.Id);

            var deletedRecord = repository.GetById(id);

            Assert.IsNull(deletedRecord);
        }
    }
}
