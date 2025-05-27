using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;

public class GetTrustyDraftReportQuery : BaseRequest, IQuery<Result<IEnumerable<GetTrustyDraftReportQueryResult>>>
{
    public string? FromDate { get; set; }
    public string? ToDate { get; set; }
    
    public DateTime? FromDateMiladi { get; set; }
    public DateTime? ToDateMiladi { get; set; }
    
    
    public string? DraftyNumber { get; set; }
    public long? CreatedBy { get; set; }
}