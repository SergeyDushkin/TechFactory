using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Systems.Security;

namespace TF.DAL
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly NoodleDbContext context;

        public UserRoleRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(UserRoleQuery.Delete(id));
            }
        }

        public IEnumerable<UserRole> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<UserRole>(UserRoleQuery.All());
            }
        }

        public UserRole GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<UserRole>(UserRoleQuery.ById(id)).SingleOrDefault();
            }
        }

        public UserRole Update(UserRole userrole)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(UserRoleQuery.Update(userrole));
                return userrole;
            }
        }

        public UserRole Create(UserRole userrole)
        {
            if (userrole.Id == Guid.Empty)
                userrole.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(UserRoleQuery.Insert(userrole));
                return userrole;
            }
        }
    }
}
