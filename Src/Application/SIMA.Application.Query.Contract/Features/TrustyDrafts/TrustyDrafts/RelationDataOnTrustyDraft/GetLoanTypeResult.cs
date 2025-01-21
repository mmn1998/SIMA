namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts.RelationDataOnTrustyDraft
{
    public class GetLoanTypeResult
    {
        public long LoanTypeId { get; set; }
        public string? LoanTypeName { get; set; }
        public string? LoanTypeCode { get; set; }
    }
}
