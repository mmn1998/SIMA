using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;

public class GetAllServiceCategoriesQuery : BaseRequest, IQuery<Result<IEnumerable<GetServiceCategoryQueryResult>>>
{
}