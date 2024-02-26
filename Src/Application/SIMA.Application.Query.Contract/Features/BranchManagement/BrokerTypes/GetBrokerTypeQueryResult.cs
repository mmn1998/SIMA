namespace SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes
{
    public class GetBrokerTypeQueryResult
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ActiveStatusId { get; set; }
        public string? ActiveStatus { get; set; }
    }
}
