using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonApi.DbLayer.Tables
{
    public class Person : CommonLibrary.BaseClasses.BaseTable
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string Company { get; set; }
        [NotMapped]
        public List<CommunicationInformation> CommunicationInformations { get; set; }
    }
}