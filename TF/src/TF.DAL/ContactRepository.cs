using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business;

namespace TF.DAL
{
    public class ContactRepository : IContactRepository
    {
        private readonly NoodleDbContext context;

        public ContactRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(ContactQuery.Delete(id));
            }
        }

        public IEnumerable<Contact> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Contact>(ContactQuery.All());
            }
        }

        public Contact GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Contact>(ContactQuery.ById(id)).SingleOrDefault();
            }
        }

        public Contact Update(Contact сontact)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(ContactQuery.Update(сontact));
                return сontact;
            }
        }

        public Contact Create(Contact сontact)
        {
            if (сontact.Id == Guid.Empty)
                сontact.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(ContactQuery.Insert(сontact));
                return сontact;
            }
        }
    }
}
