using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class GetLogisticRequestsQueryResult
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedAtPersian => DateHelper.ToPersianDateTime(CreatedAt);
    public string? CreatedBy { get; set; }
    public long ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
    public long CreatorDepartmentId { get; set; }
    public string? CreatorDepartmentName { get; set; }
    public string? IsAssigneeForced { get; set; }
    public string? IsActorManager { get; set; }
    public List<GoodsList>? GoodsList { get; set; }
    public List<DocumentList>? DocumentList { get; set; }

}



