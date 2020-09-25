using Microsoft.EntityFrameworkCore;
using ReportApi.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using CommonLibrary;

namespace ReportApi.Services
{
    public class ReportService : CommonLibrary.BaseClasses.BaseService<DatabaseContext>
    {
        private RabbitMQService rabbitMQService;
        public ReportService(RabbitMQService rabbitMQService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.rabbitMQService = rabbitMQService;
        }

        public List<DbLayer.Tables.Report> GetReports()
        {
            return db.Reports.AsNoTracking().ToList();
        }
        public DbLayer.Tables.Report GetReport(Guid reportId)
        {
            var _report = db.Reports.FirstOrDefault(x => x.Id == reportId);
            if (_report != null)
            {
                _report.ReportItems = db.ReportItems.Where(x => x.ReportId == reportId).AsNoTracking().ToList();
            }
            return _report;
        }


        public DbLayer.Tables.Report Create()
        {
            var model = new DbLayer.Tables.Report
            {
                Status = "Hazırlanıyor"
            };
            db.Reports.Add(model);
            db.SaveChanges();
            var _req = new CommonLibrary.MsmqModel.ReportRequest
            {
                ReportId = model.Id
            };

            rabbitMQService.Publish(CommonLibrary.Constants.MessageQueue.ReportCreateRequest, _req.Serialize());

            return model;
        }


    }
}
