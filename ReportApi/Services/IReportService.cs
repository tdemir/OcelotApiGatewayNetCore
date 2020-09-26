using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApi.Services
{
    public interface IReportService
    {
        List<DbLayer.Tables.Report> GetReports();
        DbLayer.Tables.Report GetReport(Guid reportId);
        DbLayer.Tables.Report Create();
    }
}
