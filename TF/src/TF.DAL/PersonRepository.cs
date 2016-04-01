using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business;

namespace TF.DAL
{
    public class PersonRepository : IPersonRepository
    {
        private readonly NoodleDbContext context;

        public PersonRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public void Delete(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(PersonQuery.Delete(id));
            }
        }

        public IEnumerable<PERSON> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<PERSON>(PersonQuery.All());
            }
        }

        public PERSON GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<PERSON>(PersonQuery.ById(id)).SingleOrDefault();
            }
        }

        public PERSON Update(PERSON person)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(PersonQuery.Update(person));
                return person;
            }
        }

        public PERSON Create(PERSON person)
        {
            if (person.ID == Guid.Empty)
                person.ID = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(PersonQuery.Insert(person));
                return person;
            }
        }
    }
}
