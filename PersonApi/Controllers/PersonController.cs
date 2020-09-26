using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonApi.DbLayer.Tables;

namespace PersonApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly Services.IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(Services.IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _personService.GetList();
        }

        [HttpGet("{id}")]
        public Person Get(Guid id)
        {
            return _personService.Get(id);
        }

        [HttpPost]
        public Person Post([FromBody] Person model)
        {
            return _personService.Add(model);
        }

        [HttpPut]
        public Person Put([FromBody] Person model)
        {
            return _personService.Update(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _personService.Delete(id);
            return new OkResult();
        }

    }
}