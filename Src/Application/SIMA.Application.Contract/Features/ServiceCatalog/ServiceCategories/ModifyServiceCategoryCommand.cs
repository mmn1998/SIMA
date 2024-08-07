using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServiceCategories;

public class ModifyServiceCategoryCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long ServiceTypeId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}
