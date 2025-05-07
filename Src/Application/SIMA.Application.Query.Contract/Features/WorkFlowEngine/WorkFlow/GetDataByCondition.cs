namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow
{
    public class GetDataByCondition 
    {
        public long? WorkFlowId { get; set; }
        public long? ProjectId { get; set; }
        public long? DomainId { get; set; }
    }
}
