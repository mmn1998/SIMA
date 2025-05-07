using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItemStatuses;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.ConfigurationItemStatuses;

public class ConfigurationItemStatusQueryHandler : IQueryHandler<GetConfigurationItemStatusQuery, Result<GetConfigurationItemStatusQueryResult>>,
    IQueryHandler<GetAllConfigurationItemStatusesQuery, Result<IEnumerable<GetConfigurationItemStatusQueryResult>>>
{
    private readonly IConfigurationItemStatusQueryRepository _repository;

    public ConfigurationItemStatusQueryHandler(IConfigurationItemStatusQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetConfigurationItemStatusQueryResult>> Handle(GetConfigurationItemStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetConfigurationItemStatusQueryResult>>> Handle(GetAllConfigurationItemStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}