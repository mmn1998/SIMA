using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Categories;

public class GetAllCategoryQuery: BaseRequest, IQuery<Result<IEnumerable<GetCategoryQueryResult>>>
{
    
}