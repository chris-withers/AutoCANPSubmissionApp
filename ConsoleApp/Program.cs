using System;
using AutoCANP.Api.Types;
using System.Threading.Tasks;
using AutoCANP.Api.BusinessLogic.Services;
using System.Collections.Generic;
using AutoCANP.Api.BusinessLogic.Utilities;
using System.Linq;

namespace ConsoleApp
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    class Program
    {

        public class WindDirTimes
        {
            public string WindDirection { get; set; }
            public double WindStrength { get; set; }
            public string Time { get; set; }

        }
        static async Task Main(string[] args)
        {
           // var sitesService = new GetSitesService();
          //  var getRaspResultService = new GetRaspResultService();

           // var sites = await sitesService.GetSitesAsync();

            var calculateFlyabilityService = new CalculateSiteFlyabilityService
            {
                GetGetRaspResultService = new GetRaspResultService(),
                GetSitesService = new GetSitesService()
            };



            /*  foreach (var site in sites.Sites)
              {
                  var raspResult = await getRaspResultService.GetRaspResultForSite(site);

                  var stats = GetFlyablePeriods(raspResult, site.WindDirections);

                  int i = 1;

              }*/

            var report = await calculateFlyabilityService.GenerateReportAsync();

            Console.WriteLine("Report Generate: " + report.Generated + " for date: " + report.CANPForDate);
            Console.WriteLine("-------------------");
            Console.WriteLine("Sites:");
            foreach (var siteReport in report.SiteReports)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Site: " + siteReport.Name);
                Console.WriteLine("Send CANP: " + siteReport.SendCANP);
                if(siteReport.SendCANP)
                {
                    Console.WriteLine("Evidence for sending CANP:");
                    foreach(var evidence in siteReport.Evidence)
                    {
                        Console.WriteLine("Time: " + evidence.Time + ", Wind Dir: " + evidence.WindDirection + ", Wind Strength (mph): " + evidence.WindStrength);
                    }
                }
                Console.WriteLine("---------------------\n\n\n");
            }

            Console.Read();
        }

        internal static List<string> GetWindDirections(List<Value> data)
        {
            var windDirections = new List<string>();

            foreach(var item in data)
            {
                windDirections.Add(Compass.DegreesToCompass(item.value));
            }

            return windDirections;
        }

        internal static List<WindDirTimes> GetFlyablePeriods(RaspResult raspResult, List<string> siteWindDirections)
        {
            var windDirections = raspResult.get_rasp_blipspot_results.Results.Find(n => n.alias == "Bl Wind Dir").values;
            var windSpeed = raspResult.get_rasp_blipspot_results.Results.Find(n => n.alias == "BL Wind Spd").values;

            var windDirTimes = new List<WindDirTimes>();

            foreach (var item in windDirections)
            {
                foreach(var dir in siteWindDirections)
                {
                    if(Compass.DegreesToCompass(item.value) == dir)
                    {
                        windDirTimes.Add(new WindDirTimes { Time = item.time, WindDirection = dir });
                    }
                }
            }

            foreach (var item in windDirTimes)
            {
                foreach (var speed in windSpeed)
                {
                    if (item.Time == speed.time)
                    {
                        item.WindStrength = speed.value;
                    }
                }
            }

            return windDirTimes;
        }
    }
}
