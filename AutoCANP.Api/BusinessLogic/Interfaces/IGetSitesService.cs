using AutoCANP.Api.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoCANP.Api.BusinessLogic.Interfaces
{
    public interface IGetSitesService
    {
        public Task<FlyingSites> GetSitesAsync();
    }
}
