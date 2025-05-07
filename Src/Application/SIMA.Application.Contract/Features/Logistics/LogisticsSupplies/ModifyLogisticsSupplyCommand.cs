using Sima.Framework.Core.Mediator;
using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.LogisticsSupplies;

public class ModifyLogisticsSupplyCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Description { get; set; }
    public string PayByFundCard { get; set; }
    public IssueInforamationEventCommand IssueInforamation { get; set; }
    public List<long>? DocumentList { get; set; }
    public List<long>? LogisticsRequestGoodsList { get; set; }
}