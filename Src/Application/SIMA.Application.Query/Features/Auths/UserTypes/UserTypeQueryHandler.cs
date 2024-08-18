using SIMA.Application.Query.Contract.Features.Auths.UserTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.UserTypes;

namespace SIMA.Application.Query.Features.Auths.UserTypes;

public class UserTypeQueryHandler : IQueryHandler<GetUserTypeQuery, Result<GetUserTypeQueryResult>>,
    IQueryHandler<GetAllUserTypesQuery, Result<IEnumerable<GetUserTypeQueryResult>>>
{
    private readonly IServiceUserTypeQueryRepository _repository;

    public UserTypeQueryHandler(IServiceUserTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetUserTypeQueryResult>> Handle(GetUserTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetUserTypeQueryResult>>> Handle(GetAllUserTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}