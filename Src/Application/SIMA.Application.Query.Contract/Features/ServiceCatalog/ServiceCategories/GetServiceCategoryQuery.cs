using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;

public class GetServiceCategoryQuery : IQuery<Result<GetServiceCategoryQueryResult>>
{
    public long Id { get; set; }
}