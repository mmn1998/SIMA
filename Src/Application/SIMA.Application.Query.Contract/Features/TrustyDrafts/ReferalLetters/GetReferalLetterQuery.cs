using SIMA.Application.Query.Contract.Features.DMS.Documents;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.ReferalLetters;

public class GetReferalLetterQuery : IQuery<Result<GetReferalLetterQueryResult>>
{
    public long Id { get; set; }
}
public class GetReferalLetterQueryByLetterNumber : IQuery<Result<GetDownloadDocumentQueryResult>>
{
    public string LetterNumber { get; set; }
}
