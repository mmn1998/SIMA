using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.DataCenters;

public class DeleteDataCenterCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}