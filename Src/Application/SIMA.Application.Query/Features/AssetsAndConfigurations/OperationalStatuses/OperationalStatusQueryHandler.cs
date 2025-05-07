using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseTypes;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.OperationalStatuses;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Contracts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.OperationalStatuses;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.OperationalStatuses;

public class OperationalStatusQueryHandler:IQueryHandler<GetOperationalStatusQuery, Result<GetOperationalStatusQueryResult>>,
    IQueryHandler<GetAllOperationalStatusQuery, Result<IEnumerable<GetOperationalStatusQueryResult>>>

{
    private readonly IOperationalStatusQueryRepository _repository;

    public OperationalStatusQueryHandler(IOperationalStatusQueryRepository repository)
    {
        _repository = repository;
    }


    public async Task<Result<GetOperationalStatusQueryResult>> Handle(GetOperationalStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetOperationalStatusQueryResult>>> Handle(GetAllOperationalStatusQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}