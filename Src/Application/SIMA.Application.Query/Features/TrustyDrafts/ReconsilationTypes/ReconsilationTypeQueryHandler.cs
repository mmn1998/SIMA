using SIMA.Application.Query.Contract.Features.TrustyDrafts.ReconsilationTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.ReconsilationTypes;

namespace SIMA.Application.Query.Features.TrustyDrafts.ReconsilationTypes;

public class ReconsilationTypeQueryHandler : IQueryHandler<GetReconsilationTypeQuery, Result<GetReconsilationTypeQueryResult>>,
    IQueryHandler<GetAllReconsilationTypesQuery, Result<IEnumerable<GetReconsilationTypeQueryResult>>>
{
    private readonly IReconsilationTypeQueryRepository _repository;

    public ReconsilationTypeQueryHandler(IReconsilationTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetReconsilationTypeQueryResult>> Handle(GetReconsilationTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetReconsilationTypeQueryResult>>> Handle(GetAllReconsilationTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}