using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServiceCategories;

public class CreateServiceCategoryCommand : ICommand<Result<long>>
{
    public long ServiceTyeId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}