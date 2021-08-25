using AutoCANP.Api.BusinessLogic.Interfaces;
using AutoCANP.Api.BusinessLogic.Utilities;
using AutoCANP.Api.BusinessLogic.Utilities.Extensions;
using AutoCANP.Api.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoCANP.Api.BusinessLogic.Services
{
    public class CalculateSiteFlyabilityService : ICalculateSiteFlyabilityService
    {
        private const int MAX_WIND_SPEED = 13;

        public IGetSitesService GetSitesService { get; set; }
        public IGetRaspResultService GetGetRaspResultService { get; set; }

        public async Task<Report> GenerateReportAsync()
        {
            var report = new Report
            {
                Generated = DateTime.Now
            };

            var sites = await GetSitesService.GetSitesAsync();

            foreach (var site in sites.Sites)
            {
                var raspResult = await GetGetRaspResultService.GetRaspResultForSite(site);

                var stats = GetFlyablePeriods(raspResult, site.WindDirections);

                var siteReport = new SiteReport
                {
                    Name = site.Name,
                    Lon = site.Lon,
                    Lat = site.Lat,
                    WindDirections = site.WindDirections,
                    ForDay = raspResult.ForDay,
                    Generated = DateTime.Now,
                    SendCANP = stats.HasContent(),
                };

                if (stats.HasContent())
                    siteReport.Evidence = stats;

                report.SiteReports.Add(siteReport);

            }

            return report;
        }

        internal static List<WindDirectionTimes> GetFlyablePeriods(RaspResult raspResult, List<string> siteWindDirections)
        {
            var windDirections = raspResult.get_rasp_blipspot_results.Results.Find(n => n.alias == "Bl Wind Dir").values;
            var windSpeed = raspResult.get_rasp_blipspot_results.Results.Find(n => n.alias == "BL Wind Spd").values;

            var windDirTimes = new List<WindDirectionTimes>();
            var windDirTimesFinal = new List<WindDirectionTimes>();

            foreach (var item in windDirections)
            {
                foreach (var dir in siteWindDirections)
                {
                    if (Compass.DegreesToCompass(item.value) == dir)
                    {
                        windDirTimes.Add(new WindDirectionTimes { Time = item.time, WindDirection = dir });
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

                        if(speed.value > -1 && speed.value <= MAX_WIND_SPEED)
                        {
                            windDirTimesFinal.Add(new WindDirectionTimes { Time = item.Time, WindDirection = item.WindDirection, WindStrength = item.WindStrength });
                        }
                    }
                }
            }

            return windDirTimesFinal;
        }
    }
}
