using SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiAuthenticationMethods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ApiAuthenticationMethods;

namespace SIMA.Application.Query.Features.ServiceCatalog.ApiAuthenticationMethods
{
    public class ApiAuthenticationMethodQueryHandler : IQueryHandler<GetApiAuthenticationMethodQuery, Result<GetApiAuthenticationMethodsQueryResult>>,
    IQueryHandler<GetAllApiAuthenticationMethodsQuery, Result<IEnumerable<GetApiAuthenticationMethodsQueryResult>>>
    {
        private readonly IApiAuthenticationMethodQueryRepository _repository;

        public ApiAuthenticationMethodQueryHandler(IApiAuthenticationMethodQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetApiAuthenticationMethodsQueryResult>> Handle(GetApiAuthenticationMethodQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetApiAuthenticationMethodsQueryResult>>> Handle(GetAllApiAuthenticationMethodsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}
