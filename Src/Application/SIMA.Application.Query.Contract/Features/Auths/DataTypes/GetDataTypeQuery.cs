using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.DataTypes;

public class GetDataTypeQuery : IQuery<Result<IEnumerable<GetDataTypeQueryResult>>>
{
}
public class GetDataTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsList { get; set; }
    public string? IsMultiSelect { get; set; }
    public string? ActiveStatus { get; set; }
}
