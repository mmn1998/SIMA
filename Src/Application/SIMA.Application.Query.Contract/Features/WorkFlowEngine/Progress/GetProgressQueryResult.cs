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
    }
}
