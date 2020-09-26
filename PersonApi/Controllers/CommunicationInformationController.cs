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
    public class CommunicationInformationController : ControllerBase
    {
        private readonly Services.ICommunicationInformationService _communicationInformationService;
        private readonly ILogger<CommunicationInformationController> _logger;

        public CommunicationInformationController(Services.ICommunicationInformationService communicationInformationService, ILogger<CommunicationInformationController> logger)
        {
            _communicationInformationService = communicationInformationService;
            _logger = logger;
        }

        [HttpGet("{personId}")]
        public List<CommunicationInformation> Get(Guid personId)
        {
            return _communicationInformationService.GetList(personId);
        }

        [HttpPost("{personId}")]
        public CommunicationInformation Post(Guid personId, [FromBody] CommunicationInformation model)
        {
            return _communicationInformationService.Add(personId, model);
        }

        [HttpDelete("{personId}/{id}")]
        public IActionResult Delete(Guid personId, Guid id)
        {
            _communicationInformationService.Delete(personId, id);
            return new OkResult();
        }
    }
}