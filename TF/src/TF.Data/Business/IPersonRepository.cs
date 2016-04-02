using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface IPersonRepository
    {
        Person Create(Person Person);
        Person Update(Person Person);
        void Delete(Guid id);

        IEnumerable<Person> GetAll();
        Person GetById(Guid id);

    }
}
