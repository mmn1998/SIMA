using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;

public class GetAllServiceCategoriesExceptThisIdQuery :  IQuery<Result<IEnumerable<GetServiceCategoryQueryResult>>>
{
    public long Id { get; set; }
}