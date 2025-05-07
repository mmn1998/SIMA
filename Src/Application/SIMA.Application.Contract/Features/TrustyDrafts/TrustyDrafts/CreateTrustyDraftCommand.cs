using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.TrustyDrafts;

public class CreateTrustyDraftCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long? InquiryRequestId { get; set; }
    public long WageDeductionMethodId { get; set; }
    public long DraftTypeId { get; set; }
    public decimal? DraftIssueWage { get; set; }
    public decimal? BuyShareFromWage { get; set; }
    public decimal? MainShareFromWage { get; set; }
    public List<CreateDraftDocument>? TrustDraftDocumentList { get; set; }
}