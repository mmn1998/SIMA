using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BranchManagement.Branches;

public class DeleteBranchCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
