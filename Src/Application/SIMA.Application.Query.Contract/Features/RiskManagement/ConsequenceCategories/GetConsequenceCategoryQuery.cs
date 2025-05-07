using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceCategories;

public class GetConsequenceCategoryQuery : IQuery<Result<GetConsequenceCategoryQueryResult>>
{
    public long Id { get; set; }
}