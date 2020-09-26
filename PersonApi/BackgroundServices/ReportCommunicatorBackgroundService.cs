using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using CommonLibrary;
using CommonLibrary.MsmqModel;
using Utility;

namespace PersonApi.BackgroundServices
{
    public class ReportCommunicatorBackgroundService : BackgroundService
    {
        private IMQService rabbitMQService;
        private DbLayer.DatabaseContext db;
        public ReportCommunicatorBackgroundService(IMQService rabbitMQService, DbLayer.DatabaseContext db)
        {
            this.rabbitMQService = rabbitMQService;
            this.db = db;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var _connection = rabbitMQService.Consume(CommonLibrary.Constants.MessageQueue.ReportCreateRequest, ReportCreateRequest);
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(100, stoppingToken).ConfigureAwait(false);
            }
            _connection.Close();
            _connection.DisposeWithoutException();
        }

        private void ReportCreateRequest(string jsonData)
        {
            Action act = () =>
            {
                var _request = jsonData.Deserialize<ReportRequest>();

                var _response = new ReportResponse();
                _response.ReportId = _request.ReportId;
                _response.Items = new List<ReportResponseItem>();

                var _locations = (from p in db.People
                                  join c in db.CommunicationInformations on p.Id equals c.PersonId
                                  select c.Location).Distinct().ToList();


                foreach (var _loc in _locations)
                {
                    var _respItem = new ReportResponseItem()
                    {
                        Location = _loc
                    };
                    _respItem.PersonCount = (from p in db.People
                                             join c in db.CommunicationInformations on p.Id equals c.PersonId
                                             where c.Location == _loc
                                             select p.Id).Distinct().Count();

                    _respItem.TelephoneNumberCount = (from p in db.People
                                                      join c in db.CommunicationInformations on p.Id equals c.PersonId
                                                      where c.Location == _loc
                                                      select c.TelephoneNumber).Distinct().Count();
                    _response.Items.Add(_respItem);
                }

                rabbitMQService.Publish(CommonLibrary.Constants.MessageQueue.ReportCreateResponse, _response.Serialize());

            };
            act.ExecuteCircuitPattern(throwException: false);
        }
    }
}