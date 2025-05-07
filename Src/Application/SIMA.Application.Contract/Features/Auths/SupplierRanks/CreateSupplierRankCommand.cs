using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.SupplierRanks;

public class CreateSupplierRankCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float Ordering { get; set; }
    public int SupplierSuccessOrderCountForm { get; set; }
    public int SupplierSuccessOrderCountTo { get; set; }
}