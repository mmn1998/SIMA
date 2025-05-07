using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.SysConfigs;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.SysConnfigs;

namespace SIMA.Application.Query.Features.Auths.SysConfigs;

public class SysConfigQueryHandler : IQueryHandler<GetSysConfigQuery, Result<GetSysConfigQueryResult>>, IQueryHandler<GetAllSysConfigQuery, Result<List<GetSysConfigQueryResult>>>
{
    private readonly ISysConfigQueryRepository _repository;
    private readonly IMapper _mapper;

    public SysConfigQueryHandler(ISysConfigQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<GetSysConfigQueryResult>> Handle(GetSysConfigQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindById(request.Id);
        var result = _mapper.Map<GetSysConfigQueryResult>(entity);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetSysConfigQueryResult>>> Handle(GetAllSysConfigQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll();
        return Result.Ok(result);
    }
}
