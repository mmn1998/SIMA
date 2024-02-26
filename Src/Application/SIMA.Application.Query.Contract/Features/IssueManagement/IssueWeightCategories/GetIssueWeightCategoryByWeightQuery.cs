using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssueWeightCategories;

public class GetIssueWeightCategoryByWeightQuery : IQuery<Result<string>>
{
    public int Weight { get; set; }
}
