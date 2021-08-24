using System.Collections.Generic;

namespace AutoCANP.Api.Types
{
    public class Site
    {
        public string Name { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }

        public List<string> WindDirections { get; set; }
    }
}
