using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.BusinessCriticalities;

public class DeleteBusinessCriticalityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}