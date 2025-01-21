using SIMA.Application.Query.Contract.Features.BranchManagement.FinancialActionTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BranchManagement.FinancialActionTypes;

namespace SIMA.Application.Query.Features.BranchManagement.FinancialActionTypes
{
    public class FinancialActionTypeQueryHandler : IQueryHandler<GetAllFinancialActionTypesQuery, Result<IEnumerable<GetFinancialActionTypeQueryResult>>>,
    IQueryHandler<GetFinancialActionTypeQuery, Result<GetFinancialActionTypeQueryResult>>
    {
        private readonly IFinancialActionTypeQueryRepository _repository;

        public FinancialActionTypeQueryHandler(IFinancialActionTypeQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<GetFinancialActionTypeQueryResult>>> Handle(GetAllFinancialActionTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetFinancialActionTypeQueryResult>> Handle(GetFinancialActionTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    
    }
}
