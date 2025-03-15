using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Back_UpMethods;

public class CreateBackupMethodCommand : ICommand<Result<long>>
{
    public string Name { get; set; }
    public string Code { get; set; }
}