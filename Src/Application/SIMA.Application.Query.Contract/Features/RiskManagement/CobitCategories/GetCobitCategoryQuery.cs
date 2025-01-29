using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.CobitCategories;

public class GetCobitCategoryQuery : IQuery<Result<GetCobitCategoryQueryResult>>
{
    public long Id { get; set; }
}