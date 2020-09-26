using PersonApi.DbLayer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public interface IPersonService
    {
        List<Person> GetList();

        Person Get(Guid id);

        Person Add(Person person);

        Person Update(Person person);

        void Delete(Guid id);
    }
}
