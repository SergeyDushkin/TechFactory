using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TF.Data.Systems.Security
{
    public interface IUserRepository
    {
        User Create(User User);
        User Update(User User);
        void Delete(Guid id);

        IEnumerable<User> GetAll();
        User GetById(Guid id);
    }
}
