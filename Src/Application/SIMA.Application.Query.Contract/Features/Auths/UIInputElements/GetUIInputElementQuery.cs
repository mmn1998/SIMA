using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.UIInputElements;

public class GetUIInputElementQuery :  IQuery<Result<GetUIInputElementQueryResult>>
{
    public long Id { get; set; }
}
