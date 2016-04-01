using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Business
{
    public interface IContactDetailService
    {
        CONTACT_DETAIL Create(CONTACT_DETAIL category_detail);
        CONTACT_DETAIL Update(CONTACT_DETAIL category_detail);
        void Delete(Guid id);

        IEnumerable<CONTACT_DETAIL> GetAll();
        CONTACT_DETAIL GetById(Guid id);
    }
}
