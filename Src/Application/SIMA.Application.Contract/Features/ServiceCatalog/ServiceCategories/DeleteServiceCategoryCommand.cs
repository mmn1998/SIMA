using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServiceCategories;

public class DeleteServiceCategoryCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
