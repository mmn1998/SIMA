using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceStatuses;

namespace SIMA.Application.Query.Features.ServiceCatalog.ServiceStatuses
{
    public class ServiceStatusQueryHandler : IQueryHandler<GetServiceStatusQuery, Result<GetServiceStatusesQueryResult>>,
    IQueryHandler<GetAllServiceStatusesQuery, Result<IEnumerable<GetServiceStatusesQueryResult>>>
    {
        private readonly IServiceStatusQueryRepository _repository;

        public ServiceStatusQueryHandler(IServiceStatusQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetServiceStatusesQueryResult>> Handle(GetServiceStatusQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetServiceStatusesQueryResult>>> Handle(GetAllServiceStatusesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}
