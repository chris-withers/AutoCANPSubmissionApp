using AutoCANP.Api.BusinessLogic.Interfaces;
using AutoCANP.Api.Types;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AutoCANP.Api.BusinessLogic.Services
{
    public class GetRaspResultService : IGetRaspResultService
    {
        public async Task<RaspResult> GetRaspResultForSite(Site site)
        {
            var client = new HttpClient();
            var tomorrow = DateTime.Now.AddDays(1).DayOfWeek;

            var endPoint = $"http://rasp.mrsap.org/cgi-bin/get_rasp_blipspot.cgi?region={tomorrow}&grid=d2&day=0&lat={site.Lat}&lon={site.Lon}&width=2000&height=2000&linfo=1&param=&format=JSON";
            var result = await client.GetFromJsonAsync<RaspResult>(endPoint);

            return result;

        }
    }
}
