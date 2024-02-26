using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Features.BranchManagement.CurrencyTypes
{
    public class CurrencyTypeQueryHandler : IQueryHandler<GetAllCurrencyTypesQuery, Result<List<GetCurrencyTypeQueryResult>>>,
    IQueryHandler<GetCurrencyTypeQuery, Result<GetCurrencyTypeQueryResult>>
    {
        private readonly ICurrencyTypeReadRepository _repository;

        public CurrencyTypeQueryHandler(ICurrencyTypeReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetCurrencyTypeQueryResult>>> Handle(GetAllCurrencyTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAll(request.Request);
            return Result.Ok(result);
        }

        public async Task<Result<GetCurrencyTypeQueryResult>> Handle(GetCurrencyTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
