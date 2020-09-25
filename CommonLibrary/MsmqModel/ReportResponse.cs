using System;
using System.Collections.Generic;

namespace CommonLibrary.MsmqModel
{
    public class ReportResponse
    {
        public Guid ReportId { get; set; }

        public List<ReportResponseItem> Items { get; set; }
    }
}