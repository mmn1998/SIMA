namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State
{
    public class GetStateQueryResult
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long WorkFlowId { get; set; }
        public string? WorkFlowName { get; set; }
        public string? DomainName { get; set; }
        public long? DomainId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectId { get; set; }
        public long ActiveStatusId { get; set; }
        public string ActiveStatus { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public long CreatedBy { get; set; }
        //public byte[]? ModifiedAt { get; set; }
        //public long ModifiedBy { get; set; }
    }
}
