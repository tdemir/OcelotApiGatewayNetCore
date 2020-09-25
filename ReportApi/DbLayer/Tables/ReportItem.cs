using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApi.DbLayer.Tables
{
    public class ReportItem : CommonLibrary.BaseClasses.BaseTable
    {
        [ForeignKey(nameof(Report))]
        public Guid ReportId { get; set; }
        public virtual Report Report { get; set; }
        [MaxLength(50)]
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int TelephoneNumberCount { get; set; }
    }
}
