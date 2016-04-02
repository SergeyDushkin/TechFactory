using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Systems.Security
{
    public interface IUserIdentityRepository
    {
        UserIdentity Create(UserIdentity User);
        UserIdentity Update(UserIdentity User);
        void Delete(Guid id);

        IEnumerable<UserIdentity> GetAll();
        UserIdentity GetById(Guid id);
    }
}
