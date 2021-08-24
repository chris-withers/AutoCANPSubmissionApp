using System;
using System.Net.Http;
using System.Net.Http.Json;
using AutoCANP.Api.Types;
using System.Threading.Tasks;
using AutoCANP.Api.BusinessLogic.Services;

namespace ConsoleApp
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    class Program
    {
        static async Task Main(string[] args)
        {
            var site = new Site
            {
                Name = "Treak Cliff",
                Lat = 53.344,
                Lon = -1.79897
            };

            var sitesService = new GetSitesService();
            var getRaspResultService = new GetRaspResultService();

            var sites = await sitesService.GetSitesAsync();

            var result = await getRaspResultService.GetRaspResultForSite(sites.Sites[0]);

            Console.WriteLine();
            Console.Read();
        }

    }
}
