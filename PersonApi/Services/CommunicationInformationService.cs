using System.Collections.Generic;
using System;
using PersonApi.DbLayer;
using PersonApi.DbLayer.Tables;
using Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PersonApi.Services
{
    public class CommunicationInformationService : CommonLibrary.BaseClasses.BaseService<DatabaseContext>, ICommunicationInformationService
    {

        private readonly Lazy<PersonService> lazyPersonService = null;

        private PersonService personServiceInstance
        {
            get
            {
                return lazyPersonService.Value;
            }
        }


        public CommunicationInformationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            lazyPersonService = new Lazy<PersonService>(() => serviceProvider.GetService<PersonService>());
        }

        public List<CommunicationInformation> GetList(Guid personId)
        {
            return db.CommunicationInformations.AsNoTracking().ToList();
        }

        public CommunicationInformation Add(Guid personId, CommunicationInformation model)
        {
            var person = personServiceInstance.Get(personId);
            if (person == null)
            {
                throw new EntryPointNotFoundException();
            }
            model.PersonId = personId;
            model.Id = System.Guid.NewGuid();
            model.CreatedDate = DateTime.UtcNow;
            db.CommunicationInformations.Add(model);
            db.SaveChanges();
            return model;
        }

        public void Delete(Guid personId, Guid id)
        {
            var person = db.CommunicationInformations.FirstOrDefault(x => x.Id == id && x.PersonId == personId);
            if (person == null)
            {
                throw new EntryPointNotFoundException();
            }
            person.IsDeleted = true;
            db.SaveChanges();
        }
    }
}