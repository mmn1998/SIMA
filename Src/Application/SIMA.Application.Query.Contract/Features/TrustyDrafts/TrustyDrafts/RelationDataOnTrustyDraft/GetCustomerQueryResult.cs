namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts.RelationDataOnTrustyDraft
{
    public class GetCustomerQueryResult
    {
        public long CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerNumber { get; set; }
    }
}
