using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Back_UpMethods;

public class GetBackupMethodQuery : IQuery<Result<GetBackupMethodQueryResult>>
{
    public long Id { get; set; }
}