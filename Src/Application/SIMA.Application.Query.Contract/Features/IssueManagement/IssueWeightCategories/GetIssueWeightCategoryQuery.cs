using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssueWeightCategories;

public class GetIssueWeightCategoryQuery : IQuery<Result<GetIssueWeightCategoryQueryResult>>
{
    public long Id { get; set; }
}
