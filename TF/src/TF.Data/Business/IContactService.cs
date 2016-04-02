using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface IContactService
    {
        Contact Create(Contact contact);
        Contact Update(Contact contact);
        void Delete(Guid id);

        IEnumerable<Contact> GetAll();
        Contact GetById(Guid id);
    }
}
