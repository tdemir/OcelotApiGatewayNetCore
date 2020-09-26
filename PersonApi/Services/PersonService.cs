using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PersonApi.DbLayer;
using PersonApi.DbLayer.Tables;

namespace PersonApi.Services
{
    public class PersonService : CommonLibrary.BaseClasses.BaseService<DatabaseContext>, IPersonService
    {
        public PersonService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }


        public List<Person> GetList()
        {
            return db.People.AsNoTracking().ToList();
        }

        public Person Get(Guid id)
        {
            return db.People.FirstOrDefault(x => x.Id == id);
        }

        public Person Add(Person person)
        {
            person.Id = System.Guid.NewGuid();
            person.CreatedDate = DateTime.UtcNow;
            db.People.Add(person);
            db.SaveChanges();
            return person;
        }

        public Person Update(Person person)
        {
            var _item = Get(person.Id);
            if (_item != null)
            {
                _item.Name = person.Name;
                _item.Surname = person.Surname;
                _item.Company = person.Company;
                db.SaveChanges();
                return _item;
            }
            return null;
        }

        public void Delete(Guid id)
        {
            var _person = Get(id);
            if (_person != null)
            {
                _person.IsDeleted = true;
                db.SaveChanges();
            }
        }
    }
}