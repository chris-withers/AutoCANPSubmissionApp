using System;
using System.Collections.Generic;

namespace AutoCANP.Api.Types
{
    public class SiteReport : Site
    {
        public List<WindDirectionTimes> Evidence { get; set; }
        public DateTime Generated { get; set; }
        public DateTime ForDay { get; set; }
        public bool SendCANP { get; set; }
    }
}
