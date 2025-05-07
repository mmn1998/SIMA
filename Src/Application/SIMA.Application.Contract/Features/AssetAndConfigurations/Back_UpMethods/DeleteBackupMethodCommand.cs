using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Back_UpMethods;

public class DeleteBackupMethodCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}