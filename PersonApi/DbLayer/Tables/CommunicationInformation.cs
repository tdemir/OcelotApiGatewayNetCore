using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonApi.DbLayer.Tables
{
    public class CommunicationInformation : CommonLibrary.BaseClasses.BaseTable
    {
        [ForeignKey(nameof(Person))]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }
        [MaxLength(20)]
        public string TelephoneNumber { get; set; }
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        [MaxLength(50)]
        public string Location { get; set; }
        [MaxLength(500)]
        public string InformationDetail { get; set; }

    }
}