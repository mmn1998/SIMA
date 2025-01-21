namespace SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyOprationTypes
{
    public class GetCurrencyOprationTypeQueryResult
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ActiveStatusId { get; set; }
        public string? ActiveStatus { get; set; }
    }
}
