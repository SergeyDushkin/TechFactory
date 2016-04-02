using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Systems.Security
{
    public interface IRoleRepository
    {
        ROLE Create(ROLE role);
        ROLE Update(ROLE role);
        void Delete(Guid id);

        IEnumerable<ROLE> GetAll();
        ROLE GetById(Guid id);
    }
}
