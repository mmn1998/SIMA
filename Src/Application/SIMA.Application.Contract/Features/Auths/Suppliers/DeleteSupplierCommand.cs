using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Suppliers;

public class DeleteSupplierCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}