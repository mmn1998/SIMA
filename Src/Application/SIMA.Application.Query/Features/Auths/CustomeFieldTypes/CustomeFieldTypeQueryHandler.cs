using SIMA.Application.Query.Contract.Features.Auths.CustomeFieldTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.CustomeFieldTypes;

namespace SIMA.Application.Query.Features.Auths.CustomeFieldTypes
{
    public class CustomeFieldTypeQueryHandler : 
    IQueryHandler<GetAllCustomeFieldTypesQuery, Result<IEnumerable<GetCustomeFieldTypeQueryResult>>>
    {
        private readonly ICustomeFieldTypeQueryRepository _repository;

        public CustomeFieldTypeQueryHandler(ICustomeFieldTypeQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<GetCustomeFieldTypeQueryResult>>> Handle(GetAllCustomeFieldTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetAll(request);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
