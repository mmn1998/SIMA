using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories
{
    public class GetAllServiceCategoriesByServiceTypeIdQuery : IQuery<Result<IEnumerable<GetServiceCategoryQueryResult>>>
    {
        public long ServiceTypeId { get; set; }
    }
}
