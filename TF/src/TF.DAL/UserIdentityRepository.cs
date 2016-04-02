using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Systems.Security;

namespace TF.DAL
{
    public class UserIdentityRepository : IUserIdentityRepository
    {
        private readonly NoodleDbContext context;

        public UserIdentityRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(UserIdentityQuery.Delete(id));
            }
        }

        public IEnumerable<UserIdentity> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<UserIdentity>(UserIdentityQuery.All());
            }
        }

        public UserIdentity GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<UserIdentity>(UserIdentityQuery.ById(id)).SingleOrDefault();
            }
        }

        public UserIdentity Update(UserIdentity user)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(UserIdentityQuery.Update(user));
                return user;
            }
        }

        public UserIdentity Create(UserIdentity user)
        {
            if (user.Id == Guid.Empty)
                user.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(UserIdentityQuery.Insert(user));
                return user;
            }
        }
    }
}
