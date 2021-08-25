using AutoCANP.Api.Types;
using System.Threading.Tasks;

namespace AutoCANP.Api.BusinessLogic.Interfaces
{
    public interface ICalculateSiteFlyabilityService
    {
        public Task<Report> GenerateReportAsync();
    }
}
