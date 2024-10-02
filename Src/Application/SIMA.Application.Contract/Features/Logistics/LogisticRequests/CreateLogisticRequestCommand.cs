using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;
using SIMA.Application.Contract.Features.IssueManagement.Issues;

namespace SIMA.Application.Contract.Features.Logistics.LogisticRequests;

public class CreateLogisticRequestCommand : ICommand<Result<long>>
{
    public string Description { get; set; }
    public long RequesterId { get; set; }
    public List<LogisticRequestGoodsCommand> GoodsList { get; set; }
    public IssueInforamationEventCommand IssueInforamation { get; set; }
    public List<LogisticRequestDocumentCommand> Documents { get; set; }
}

