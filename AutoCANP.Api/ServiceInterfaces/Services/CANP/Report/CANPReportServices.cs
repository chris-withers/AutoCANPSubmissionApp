using ServiceStack;
using AutoCANP.Api.ServiceModel.Dtos.CANP.Report;
using System.Threading.Tasks;
using AutoCANP.Api.BusinessLogic.Interfaces;

namespace AutoCANP.Api.ServiceInterfaces.Services.CANP.Report
{
    public class CANPReportServices : Service
    {
        public ICalculateSiteFlyabilityService CalculateSiteFlyabilityService { get; set; }
        [CacheResponse(Duration = 3360)]
        public async Task<GetCANPReportResponse> Get(GetCANPReport request)
        {
            var result = await CalculateSiteFlyabilityService.GenerateReportAsync();

            return new GetCANPReportResponse
            {
                Report = result
            };
        }
    }
}
