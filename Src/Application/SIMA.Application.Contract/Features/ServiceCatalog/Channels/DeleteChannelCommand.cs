using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.Channels;

public class DeleteChannelCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
