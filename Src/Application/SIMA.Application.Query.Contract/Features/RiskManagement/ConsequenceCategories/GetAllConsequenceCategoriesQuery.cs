using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceCategories;

public class GetAllConsequenceCategoriesQuery : BaseRequest, IQuery<Result<IEnumerable<GetConsequenceCategoryQueryResult>>>
{
}