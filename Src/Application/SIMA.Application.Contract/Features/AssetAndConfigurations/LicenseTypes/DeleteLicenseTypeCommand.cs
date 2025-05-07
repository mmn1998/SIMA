using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.LicenseTypes;

public class DeleteLicenseTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}