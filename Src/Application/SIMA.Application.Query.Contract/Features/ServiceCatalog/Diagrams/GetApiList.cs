using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetApiList:  IQuery<Result<List<GetApiListResult>>>
{
    public string? Type { get; set; }
    public long? Id { get; set; }
}