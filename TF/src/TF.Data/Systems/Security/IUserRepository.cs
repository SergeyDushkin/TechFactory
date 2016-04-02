using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Systems.Security
{
    public interface IUserRepository
    {
        USER Create(USER User);
        USER Update(USER User);
        void Delete(Guid id);

        IEnumerable<USER> GetAll();
        USER GetById(Guid id);
    }
}
