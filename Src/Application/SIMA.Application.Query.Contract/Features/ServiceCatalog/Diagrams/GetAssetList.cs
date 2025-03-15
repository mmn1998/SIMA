using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetAssetList :  IQuery<Result<List<GetAssetListResult>>>
{
    public string? Type { get; set; }
    public long? Id { get; set; }
}