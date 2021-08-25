using System;
using System.Collections.Generic;

namespace AutoCANP.Api.Types
{
    public class Report
    {
        public  Report()
        {
            SiteReports = new List<SiteReport>();
        }
        public DateTime Generated { get; set; }
        public DateTime CANPForDate { get; set; }
        public List<SiteReport> SiteReports { get; set; }
    }
}
