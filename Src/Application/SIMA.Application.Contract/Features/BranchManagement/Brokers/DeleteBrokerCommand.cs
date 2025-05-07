using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BranchManagement.Brokers;

public class DeleteBrokerCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
