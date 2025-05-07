using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BranchManagement.LoanTypes;

public class DeleteLoanTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}