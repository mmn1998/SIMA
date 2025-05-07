using SIMA.Application.Query.Contract.Features.BranchManagement.FinancialSuppliers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BranchManagement.FinancialSuppliers;

namespace SIMA.Application.Query.Features.BranchManagement.FinancialSuppliers
{
    public class FinancialSupplierQueryHandler : IQueryHandler<GetAllFinancialSuppliersQuery, Result<IEnumerable<GetFinancialSupplierQueryResult>>>,
    IQueryHandler<GetFinancialSupplierQuery, Result<GetFinancialSupplierQueryResult>>
    {
        private readonly IFinancialSupplierQueryRepository _repository;

        public FinancialSupplierQueryHandler(IFinancialSupplierQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<GetFinancialSupplierQueryResult>>> Handle(GetAllFinancialSuppliersQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetFinancialSupplierQueryResult>> Handle(GetFinancialSupplierQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
