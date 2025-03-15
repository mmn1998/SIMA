using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataCenters;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.DataCenters;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.DataCenters;

public class DataCenterQueryHandler : IQueryHandler<GetDataCenterQuery, Result<GetDataCenterQueryResult>>,
    IQueryHandler<GetAllDataCentersQuery, Result<IEnumerable<GetDataCenterQueryResult>>>
{
    private readonly IDataCenterQueryRepository _repository;

    public DataCenterQueryHandler(IDataCenterQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetDataCenterQueryResult>> Handle(GetDataCenterQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }
    public async Task<Result<IEnumerable<GetDataCenterQueryResult>>> Handle(GetAllDataCentersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}