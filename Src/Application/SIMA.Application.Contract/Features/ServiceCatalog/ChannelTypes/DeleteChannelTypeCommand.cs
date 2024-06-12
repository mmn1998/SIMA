using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ChannelTypes;

public class DeleteChannelTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
