using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface IContactDetailRepository
    {
        ContactDetail Create(ContactDetail category_detail);
        ContactDetail Update(ContactDetail category_detail);
        void Delete(Guid id);

        IEnumerable<ContactDetail> GetAll();
        ContactDetail GetById(Guid id);
    }
}
