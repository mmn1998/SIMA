using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.CustomerTypes;

public class DeleteCustomerTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}