using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssueWeightCategories;

public class GetAllIssueWeightCategoriesQuery : BaseRequest, IQuery<Result<IEnumerable<GetIssueWeightCategoryQueryResult>>>
{
}