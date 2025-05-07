using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedureTypes;

public class GetDataProcedureTypeQuery : IQuery<Result<GetDataProcedureTypeQueryResult>>
{
    public long Id { get; set; }
}