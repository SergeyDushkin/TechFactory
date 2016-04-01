using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface IPersonRepository
    {
        PERSON Create(PERSON category);
        PERSON Update(PERSON category);
        void Delete(Guid id);

        IEnumerable<PERSON> GetAll();
        PERSON GetById(Guid id);

    }
}
