using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAs;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.MatrixAs;

namespace SIMA.Application.Query.Features.RiskManagement.MatrixAs;

public class MatrixAQueryHandler : IQueryHandler<GetMatrixAQuery, Result<GetMatrixAQueryResult>>,
    IQueryHandler<GetAllMatrixAsQuery, Result<IEnumerable<GetMatrixAQueryResult>>>
{
    private readonly IMatrixAQueryRepository _repository;

    public MatrixAQueryHandler(IMatrixAQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetMatrixAQueryResult>> Handle(GetMatrixAQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetMatrixAQueryResult>>> Handle(GetAllMatrixAsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}