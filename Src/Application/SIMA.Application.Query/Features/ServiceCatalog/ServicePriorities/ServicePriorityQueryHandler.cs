using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServicePriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServicePriorities;

namespace SIMA.Application.Query.Features.ServiceCatalog.ServicePriorities
{
    public class ServicePriorityQueryHandler : IQueryHandler<GetServicePriorityQuery, Result<GetAllServicePrioritiesQueryResult>>,
    IQueryHandler<GetAllServicePrioritiesQuery, Result<IEnumerable<GetAllServicePrioritiesQueryResult>>>
    {
        private readonly IServicePriorityQueryRepository _repository;

        public ServicePriorityQueryHandler(IServicePriorityQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetAllServicePrioritiesQueryResult>> Handle(GetServicePriorityQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetAllServicePrioritiesQueryResult>>> Handle(GetAllServicePrioritiesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}
