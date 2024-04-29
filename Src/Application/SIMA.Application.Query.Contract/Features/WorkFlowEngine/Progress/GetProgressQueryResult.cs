namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.Progress
{
    public class GetProgressQueryResult
    {
        public long Id { get; set; }
        public long StateId { get; set; }
        public string? Name { get; set; }
        public string? StatusName { get; set; }
        public int? ActiveStatusId { get; set; }
        public string? ActiveStatus { get; set; }
        public string? WorkFlowName { get; set; }
        public long? WorkFlowId { get; set; }
        public string? DomainName { get; set; }
        public long? DomainID { get; set; }
        public string? ProjectName { get; set; }
        public long? ProjectId { get; set; }
    }
}
