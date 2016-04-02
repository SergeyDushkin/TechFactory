using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Systems.Security;

namespace TF.DAL
{
    public class RoleRepository : IRoleRepository
    {
        private readonly NoodleDbContext context;

        public RoleRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(RoleQuery.Delete(id));
            }
        }

        public IEnumerable<Role> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Role>(RoleQuery.All());
            }
        }

        public Role GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Role>(RoleQuery.ById(id)).SingleOrDefault();
            }
        }

        public Role Update(Role role)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(RoleQuery.Update(role));
                return role;
            }
        }

        public Role Create(Role role)
        {
            if (role.Id == Guid.Empty)
                role.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(RoleQuery.Insert(role));
                return role;
            }
        }
    }
}
