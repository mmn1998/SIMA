using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.DataProcedures;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.DataProcedures;

public class DataProcedureQueryHandler : IQueryHandler<GetDataProcedureQuery, Result<GetDataProcedureQueryResult>>,
    IQueryHandler<GetAllDataProceduresQuery, Result<IEnumerable<GetDataProcedureQueryResult>>>
{
    private readonly IDataProcedureQueryRepository _repository;

    public DataProcedureQueryHandler(IDataProcedureQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetDataProcedureQueryResult>> Handle(GetDataProcedureQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }
    public async Task<Result<IEnumerable<GetDataProcedureQueryResult>>> Handle(GetAllDataProceduresQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}