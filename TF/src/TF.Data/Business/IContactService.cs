using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface IContactService
    {
        CONTACT Create(CONTACT contact);
        CONTACT Update(CONTACT contact);
        void Delete(Guid id);

        IEnumerable<CONTACT> GetAll();
        CONTACT GetById(Guid id);
    }
}
