using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.TrustyDrafts;

public class ModifyTrustyDraftCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long? InquiryRequestId { get; set; }
    public long WageDeductionMethodId { get; set; }
    public long DraftTypeId { get; set; }
    public decimal? DraftIssueWage { get; set; }
    public List<CreateDraftDocument>? TrustDraftDocumentList { get; set; }

}
