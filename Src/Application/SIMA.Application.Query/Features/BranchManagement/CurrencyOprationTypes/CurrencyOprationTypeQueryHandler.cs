using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyOprationTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BranchManagement.CurrencyOprationTypes;

namespace SIMA.Application.Query.Features.BranchManagement.CurrencyOprationTypes
{
    public class CurrencyOprationTypeQueryHandler : IQueryHandler<GetAllCurrencyOprationTypesQuery, Result<IEnumerable<GetCurrencyOprationTypeQueryResult>>>,
    IQueryHandler<GetCurrencyOprationTypeQuery, Result<GetCurrencyOprationTypeQueryResult>>
    {
        private readonly ICurrencyOprationTypeQueryRepository _repository;

        public CurrencyOprationTypeQueryHandler(ICurrencyOprationTypeQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<GetCurrencyOprationTypeQueryResult>>> Handle(GetAllCurrencyOprationTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetCurrencyOprationTypeQueryResult>> Handle(GetCurrencyOprationTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
