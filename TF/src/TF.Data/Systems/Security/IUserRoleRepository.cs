using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Systems.Security
{
    public interface IUserRoleRepository
    {
        UserRole Create(UserRole UserRole);
        UserRole Update(UserRole UserRole);
        void Delete(Guid id);

        IEnumerable<UserRole> GetAll();
        UserRole GetById(Guid id);
    }
}
