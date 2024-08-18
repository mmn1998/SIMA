using SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ApiTypes;

namespace SIMA.Application.Query.Features.ServiceCatalog.ApiTypes
{
    public class ApiTypeQueryHandler : IQueryHandler<GetApiTypeQuery, Result<GetApiTypesQueryResult>>,
    IQueryHandler<GetAllApiTypesQuery, Result<IEnumerable<GetApiTypesQueryResult>>>
    {
        private readonly IApiTypeQueryRepository _repository;

        public ApiTypeQueryHandler(IApiTypeQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetApiTypesQueryResult>> Handle(GetApiTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetApiTypesQueryResult>>> Handle(GetAllApiTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}
