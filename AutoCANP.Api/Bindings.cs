using AutoCANP.Api.BusinessLogic.Interfaces;
using AutoCANP.Api.BusinessLogic.Services;
using Funq;

namespace AutoCANP.Api
{
    public static class Bindings
    {
        public static void Configure(Container container)
        {
            container.Register<IGetSitesService>(c => new GetSitesService());
            container.Register<IGetRaspResultService>(c => new GetRaspResultService());
            container.Register<ICalculateSiteFlyabilityService>(c => new CalculateSiteFlyabilityService
            {
                GetRaspResultService = c.Resolve<IGetRaspResultService>(),
                GetSitesService = c.Resolve<IGetSitesService>()
            });
        }
    }
}
