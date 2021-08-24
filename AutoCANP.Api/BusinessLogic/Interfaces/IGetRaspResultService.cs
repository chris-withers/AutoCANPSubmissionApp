using AutoCANP.Api.Types;
using System.Threading.Tasks;

namespace AutoCANP.Api.BusinessLogic.Interfaces
{
    public interface IGetRaspResultService
    {
        public Task<RaspResult> GetRaspResultForSite(Site site);
    }
}
