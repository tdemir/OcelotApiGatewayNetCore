using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportApi.DbLayer.Tables
{
    public class Report : CommonLibrary.BaseClasses.BaseTable
    {
        [MaxLength(30)]
        public string Status { get; set; }

        [NotMapped]
        public List<ReportItem> ReportItems { get; set; }
    }
}