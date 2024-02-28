using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.Domains;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Domains;

namespace SIMA.Application.Query.Features.Auths.Domains;

public class DomainQueryHandler : IQueryHandler<GetDomainQuery, Result<GetDomainQueryResult>>, IQueryHandler<GetAllDomainQuery, Result<List<GetDomainQueryResult>>>
{
    private readonly IDomainQueryRepository _repository;
    private readonly IMapper _mapper;

    public DomainQueryHandler(IDomainQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Result<GetDomainQueryResult>> Handle(GetDomainQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindById(request.Id);
        var result = _mapper.Map<GetDomainQueryResult>(entity);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetDomainQueryResult>>> Handle(GetAllDomainQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll();
        return Result.Ok(result);

    }
}
