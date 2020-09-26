using PersonApi.DbLayer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public interface ICommunicationInformationService
    {
        List<CommunicationInformation> GetList(Guid personId);

        CommunicationInformation Add(Guid personId, CommunicationInformation model);

        void Delete(Guid personId, Guid id);
    }
}
