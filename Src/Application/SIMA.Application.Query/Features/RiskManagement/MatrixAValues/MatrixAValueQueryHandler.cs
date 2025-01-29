using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAs;
using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.MatrixAValues;


namespace SIMA.Application.Query.Features.RiskManagement.MatrixAValues;

public class MatrixAValueQueryHandler: IQueryHandler<GetMatrixAValueQuery, Result<GetMatrixAValueQueryResult>>,
    IQueryHandler<GetAllMatrixAValuesQuery, Result<IEnumerable<GetMatrixAValueQueryResult>>>
{
    private readonly IMatrixAValueQueryRepository _repository;

    public MatrixAValueQueryHandler(IMatrixAValueQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetMatrixAValueQueryResult>> Handle(GetMatrixAValueQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetMatrixAValueQueryResult>>> Handle(GetAllMatrixAValuesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}