using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BranchManagement.FinancialSuppliers
{
    public class ModifyFinancialSupplierCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long CustomerId { get; set; }
    }
}
