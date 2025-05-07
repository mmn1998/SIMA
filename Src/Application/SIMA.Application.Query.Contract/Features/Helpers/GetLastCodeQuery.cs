using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Helpers;

public class GetLastCodeQuery : IQuery<Result<GetLastCodeQueryResult>>
{
    public string? Type { get; set; }
}