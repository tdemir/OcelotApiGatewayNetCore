using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utility;
using CommonLibrary;
using CommonLibrary.MsmqModel;

namespace ReportApi.BackgroundServices
{
    public class ReportConsumerBackgroundService : BackgroundService
    {
        private RabbitMQService rabbitMQService;
        private DbLayer.DatabaseContext db;
        public ReportConsumerBackgroundService(RabbitMQService rabbitMQService, DbLayer.DatabaseContext db)
        {
            this.rabbitMQService = rabbitMQService;
            this.db = db;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var _connection = rabbitMQService.Consume(CommonLibrary.Constants.MessageQueue.ReportCreateResponse, ReportCreateResponse);
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(100).ConfigureAwait(false);
            }
            _connection.Close();
            _connection.DisposeWithoutException();
        }



        private void ReportCreateResponse(string jsonData)
        {
            Action act = () =>
            {
                var _model = jsonData.Deserialize<ReportResponse>();

                var _report = db.Reports.FirstOrDefault(x => x.Id == _model.ReportId);
                if (_report != null)
                {
                    _report.Status = "Tamamlandı";
                    if (_model.Items != null)
                    {
                        foreach (var item in _model.Items)
                        {
                            db.ReportItems.Add(new DbLayer.Tables.ReportItem()
                            {
                                ReportId = _model.ReportId,
                                Location = item.Location,
                                PersonCount = item.PersonCount,
                                TelephoneNumberCount = item.TelephoneNumberCount
                            });
                        }
                    }
                    db.SaveChanges();
                }

            };
            act.ExecuteCircuitPattern(throwException: false);
        }
    }
}
