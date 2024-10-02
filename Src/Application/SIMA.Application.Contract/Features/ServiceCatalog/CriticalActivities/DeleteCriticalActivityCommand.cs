using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.CriticalActivities;

public class DeleteCriticalActivityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}

