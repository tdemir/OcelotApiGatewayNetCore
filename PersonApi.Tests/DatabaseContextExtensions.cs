using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using Xunit;

namespace PersonApi.Tests
{
    public static class DatabaseContextExtensions
    {
        public static void Seed(this PersonApi.DbLayer.DatabaseContext db)
        {
            var _person = new PersonApi.DbLayer.Tables.Person()
            {
                Id = new Guid("AF34F40D-9803-4113-808D-687A46557892"),
                Name = "testName",
                Surname = "surname"
            };
            var isAdded = db.People.Any(x => x.Id == _person.Id);
            if (!isAdded)
            {
                db.People.Add(_person);
                db.SaveChanges();
            }
        }
    }
}