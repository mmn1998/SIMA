namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Modify
{
    public class ModifyWorkFlowActorArg
    {
        
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ActiveStatusId { get; set; }
        public string? IsDirectManagerOfIssueCreator { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }

    }
}
