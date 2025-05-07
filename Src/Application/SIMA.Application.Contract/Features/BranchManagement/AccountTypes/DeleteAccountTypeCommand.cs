using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BranchManagement.AccountTypes;

public class DeleteAccountTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}