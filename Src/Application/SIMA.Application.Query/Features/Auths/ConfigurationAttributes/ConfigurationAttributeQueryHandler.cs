using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.ConfigurationAttributes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.ConfigurationAttributes;

namespace SIMA.Application.Query.Features.Auths.ConfigurationAttributes;

public class ConfigurationAttributeQueryHandler : IQueryHandler<GetConfigurationAttributeQuery, Result<GetConfigurationAttributeQueryResult>>, IQueryHandler<GetAllConfigurationAttributes, Result<List<GetConfigurationAttributeQueryResult>>>
{
    private readonly IConfigurationAttributeQueryRepository _repository;
    private readonly IMapper _mapper;

    public ConfigurationAttributeQueryHandler(IConfigurationAttributeQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Result<GetConfigurationAttributeQueryResult>> Handle(GetConfigurationAttributeQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindById((int)request.Id);
        return Result.Ok(_mapper.Map<GetConfigurationAttributeQueryResult>(entity));
    }

    public async Task<Result<List<GetConfigurationAttributeQueryResult>>> Handle(GetAllConfigurationAttributes request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAll();
        return Result.Ok(entity);
    }
}
