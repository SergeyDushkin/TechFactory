using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Systems.Security
{
    public interface IUserRoleService
    {
        USER_ROLE Create(USER_ROLE User_role);
        USER_ROLE Update(USER_ROLE User_role);
        void Delete(Guid id);

        IEnumerable<USER_ROLE> GetAll();
        USER_ROLE GetById(Guid id);
    }
}
