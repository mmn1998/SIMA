using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BranchManagement.FinancialSuppliers
{
    public class DeleteFinancialSupplierCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
