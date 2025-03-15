using SIMA.Application.Query.Contract.Features.BCP.BiaValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.BiaValues;

namespace SIMA.Application.Query.Features.BCP.BiaValues;

public class BiaValueQueryHandler : IQueryHandler<GetBiaValueQuery, Result<GetBiaValueQueryResult>>,
    IQueryHandler<GetAllBiaValuesQuery, Result<IEnumerable<GetBiaValueQueryResult>>>
{
    private readonly IBiaValueQueryRepository _repository;

    public BiaValueQueryHandler(IBiaValueQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetBiaValueQueryResult>> Handle(GetBiaValueQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetBiaValueQueryResult>>> Handle(GetAllBiaValuesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}