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

        public IEnumerable<User> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<User>(UserQuery.All());
            }
        }

        public User GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<User>(UserQuery.ById(id)).SingleOrDefault();
            }
        }

        public User Update(User user)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(UserQuery.Update(user));
                return user;
            }
        }

        public User Create(User user)
        {
            if (user.Id == Guid.Empty)
                user.Id = Guid.NewGuid();

            if (user.LastLogin == DateTime.MinValue)
                user.LastLogin = DateTime.Now;

            using (var connection = context.CreateConnection())
            {
                connection.Execute(UserQuery.Insert(user));
                return user;
            }
        }
    }
}
