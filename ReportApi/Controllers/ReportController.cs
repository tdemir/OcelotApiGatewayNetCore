using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ReportApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {



        private readonly Services.ReportService _reportService;
        private readonly ILogger<ReportController> _logger;

        public ReportController(Services.ReportService reportService, ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        [HttpGet]
        public List<DbLayer.Tables.Report> Get()
        {
            return _reportService.GetReports();
        }
        [HttpGet("{reportId}")]
        public DbLayer.Tables.Report Get(Guid reportId)
        {
            return _reportService.GetReport(reportId);
        }

        [HttpPost("create")]
        public DbLayer.Tables.Report Post()
        {
            return _reportService.Create();
        }
    }
}