using ServiceStack;

namespace AutoCANP.Api.ServiceModel.Dtos.CANP.Report
{
    [Route("/CANP/Report", "GET")]
    public class GetCANPReport : IReturn<GetCANPReportResponse>
    {
    }

    public class GetCANPReportResponse : IHasResponseStatus
    {
        public Types.Report Report { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
