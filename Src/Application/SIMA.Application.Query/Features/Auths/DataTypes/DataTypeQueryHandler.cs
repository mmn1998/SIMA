using SIMA.Application.Query.Contract.Features.Auths.DataTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.DataTypes;

namespace SIMA.Application.Query.Features.Auths.DataTypes;

public class DataTypeQueryHandler : IQueryHandler<GetDataTypeQuery, Result<IEnumerable<GetDataTypeQueryResult>>>
{
    private readonly IDataTypeQueryRepository _repository;

    public DataTypeQueryHandler(IDataTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetDataTypeQueryResult>>> Handle(GetDataTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll(request);
        return Result.Ok(result);
    }
}