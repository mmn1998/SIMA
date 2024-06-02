using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServiceUserTypes;

public class DeleteServiceUserTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}