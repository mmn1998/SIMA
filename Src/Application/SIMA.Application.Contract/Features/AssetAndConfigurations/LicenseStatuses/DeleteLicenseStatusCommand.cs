using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.LicenseStatuses;

public class DeleteLicenseStatusCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}