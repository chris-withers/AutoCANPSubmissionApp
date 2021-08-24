using System;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using AutoCANP.Api.Types;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

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

            var result = await GetResponseAsync(site);

            Console.WriteLine();
            Console.Read();
        }

        public static async Task<Root> GetResponseAsync(Site site)
        {
            var services = new ServiceCollection();
            services.AddHttpClient();
            var serviceProvider = services.BuildServiceProvider();

            var tomorrow = DateTime.Now.AddDays(1).DayOfWeek;
            var client = serviceProvider.GetService<HttpClient>();

            var endPoint = $"http://rasp.mrsap.org/cgi-bin/get_rasp_blipspot.cgi?region={tomorrow}&grid=d2&day=0&lat={site.Lat}&lon={site.Lon}&width=2000&height=2000&linfo=1&param=&format=JSON";
            var vale = await client.GetFromJsonAsync<Root>(endPoint);

            return vale;
        }



    }
}
