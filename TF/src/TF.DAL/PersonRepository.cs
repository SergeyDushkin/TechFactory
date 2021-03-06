﻿using System;
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

        public IEnumerable<Person> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Person>(PersonQuery.All());
            }
        }

        public Person GetById(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return connection.Query<Person>(PersonQuery.ById(id)).SingleOrDefault();
            }
        }

        public Person Update(Person person)
        {
            using (var connection = context.CreateConnection())
            {
                connection.Execute(PersonQuery.Update(person));
                return person;
            }
        }

        public Person Create(Person person)
        {
            if (person.Id == Guid.Empty)
                person.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                connection.Execute(PersonQuery.Insert(person));
                return person;
            }
        }
    }
}
