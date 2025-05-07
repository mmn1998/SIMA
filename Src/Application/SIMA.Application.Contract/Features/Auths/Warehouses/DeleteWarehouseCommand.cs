using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Warehouses;

public class DeleteWarehouseCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}