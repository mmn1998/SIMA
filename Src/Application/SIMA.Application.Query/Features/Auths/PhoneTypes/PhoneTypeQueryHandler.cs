using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.PhoneTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.PhoneTypes;

namespace SIMA.Application.Query.Features.Auths.PhoneTypes;

public class PhoneTypeQueryHandler : IQueryHandler<GetPhoneTypeQuery, Result<GetPhoneTypeQueryResult>>, IQueryHandler<GetAllPhoneTypesQuery, Result<IEnumerable<GetPhoneTypeQueryResult>>>
{
    private readonly IPhoneTypeQueryRepository _repository;
    private readonly IMapper _mapper;

    public PhoneTypeQueryHandler(IPhoneTypeQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<GetPhoneTypeQueryResult>>> Handle(GetAllPhoneTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetPhoneTypeQueryResult>> Handle(GetPhoneTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.FindById(request.Id);
        return Result.Ok(result);
    }
}
