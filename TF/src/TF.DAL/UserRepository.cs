using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Systems.Security;

namespace TF.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly NoodleDbContext context;

        public UserRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(UserQuery.Delete(id));
            }
        }

        public IEnumerable<USER> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<USER>(UserQuery.All());
            }
        }

        public USER GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<USER>(UserQuery.ById(id)).SingleOrDefault();
            }
        }

        public USER Update(USER user)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(UserQuery.Update(user));
                return user;
            }
        }

        public USER Create(USER user)
        {
            if (user.ID == Guid.Empty)
                user.ID = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(UserQuery.Insert(user));
                return user;
            }
        }
    }
}
