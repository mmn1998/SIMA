using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedureTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.DataProcedureTypes;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.DataProcedureTypes;

public class DataProcedureTypeQueryHandler : IQueryHandler<GetDataProcedureTypeQuery, Result<GetDataProcedureTypeQueryResult>>,
    IQueryHandler<GetAllDataProcedureTypesQuery, Result<IEnumerable<GetDataProcedureTypeQueryResult>>>
{
    private readonly IDataProcedureTypeQueryRepository _repository;

    public DataProcedureTypeQueryHandler(IDataProcedureTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetDataProcedureTypeQueryResult>> Handle(GetDataProcedureTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }
    public async Task<Result<IEnumerable<GetDataProcedureTypeQueryResult>>> Handle(GetAllDataProcedureTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}