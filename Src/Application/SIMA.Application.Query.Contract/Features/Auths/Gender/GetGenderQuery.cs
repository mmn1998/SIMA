using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Gender;

public class GetGenderQuery : IQuery<Result<GetGenderQueryResult>>
{
    public long Id { get; set; }
}
