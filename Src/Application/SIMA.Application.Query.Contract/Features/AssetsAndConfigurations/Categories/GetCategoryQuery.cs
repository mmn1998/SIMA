using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Categories;

public class GetCategoryQuery: IQuery<Result<GetCategoryQueryResult>>
{
    public long Id { get; set; }
}