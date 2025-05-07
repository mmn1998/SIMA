using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BranchManagement.Customers;

public class CreateCustomerCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? CustomerNumber { get; set; }
}