using SIMA.Application.Query.Contract.Features.ServiceCatalog.ImportanceDegrees;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.ImportanceDegrees;

namespace SIMA.Application.Query.Features.BCP.ImportanceDegrees;

public class ImportanceDegreeQueryHandler : IQueryHandler<GetImportanceDegreeQuery, Result<GetImportanceDegreeQueryResult>>,
    IQueryHandler<GetAllImportanceDegreesQuery, Result<IEnumerable<GetImportanceDegreeQueryResult>>>
{
    private readonly IImportanceDegreeQueryRepository _repository;

    public ImportanceDegreeQueryHandler(IImportanceDegreeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetImportanceDegreeQueryResult>> Handle(GetImportanceDegreeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetImportanceDegreeQueryResult>>> Handle(GetAllImportanceDegreesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}