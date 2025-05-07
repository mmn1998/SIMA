using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.Logistics.LogisticRequests;

public class ModifyLogisticsRequestCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Description { get; set; }
    public List<LogisticRequestGoodsCommand> GoodsList { get; set; }
    public IssueInforamationEventCommand IssueInforamation { get; set; }
    public List<LogisticRequestDocumentCommand> Documents { get; set; }
}
