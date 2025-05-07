using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Apis;

public class DeleteApiCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}