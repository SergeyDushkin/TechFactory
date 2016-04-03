using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business;

namespace TF.DAL
{
    public class ContactDetailRepository : IContactDetailRepository
    {
        private readonly NoodleDbContext context;

        public ContactDetailRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(ContactDetailQuery.Delete(id));
            }
        }

        public IEnumerable<ContactDetail> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<ContactDetail>(ContactDetailQuery.All());
            }
        }

        public ContactDetail GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<ContactDetail>(ContactDetailQuery.ById(id)).SingleOrDefault();
            }
        }

        public ContactDetail Update(ContactDetail contactdetail)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(ContactDetailQuery.Update(contactdetail));
                return contactdetail;
            }
        }

        public ContactDetail Create(ContactDetail contactdetail)
        {
            if (contactdetail.Id == Guid.Empty)
                contactdetail.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(ContactDetailQuery.Insert(contactdetail));
                return contactdetail;
            }
        }
    }
}
