using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Companies;

public class GetCompanyByIdQuery : IQuery<Result<GetCompanyQueryResult>>
{
    public long Id { get; set; }
}
