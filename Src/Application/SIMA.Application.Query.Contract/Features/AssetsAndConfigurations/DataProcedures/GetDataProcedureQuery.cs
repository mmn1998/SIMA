using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;

public class GetDataProcedureQuery : IQuery<Result<GetDataProcedureQueryResult>>
{
    public long Id { get; set; }
}