using AutoCANP.Api.BusinessLogic.Interfaces;
using AutoCANP.Api.Types;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoCANP.Api.BusinessLogic.Services
{
    public class GetSitesService : IGetSitesService
    {
        public async Task<FlyingSites> GetSitesAsync()
        {
            //todo: env variable rather than hardcode
            var fileName = "./Data/sites.json";
            using FileStream openStream = File.OpenRead(fileName);

            return await JsonSerializer.DeserializeAsync<FlyingSites>(openStream);
        }
    }
}
