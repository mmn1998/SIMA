using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;

public class GetApiQuery : IQuery<Result<GetApiQueryResult>>
{
    public long Id { get; set; }
}