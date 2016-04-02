using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Systems.Security
{
    public interface IRoleRepository
    {
        Role Create(Role role);
        Role Update(Role role);
        void Delete(Guid id);

        IEnumerable<Role> GetAll();
        Role GetById(Guid id);
    }
}
