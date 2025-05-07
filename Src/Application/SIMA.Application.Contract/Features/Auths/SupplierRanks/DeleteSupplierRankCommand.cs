using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.SupplierRanks;

public class DeleteSupplierRankCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}