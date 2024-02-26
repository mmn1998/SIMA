namespace SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.Args
{
    public class CreateActionTypeArg
    {
        public string MainType { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? EventTag { get; set; }
        public string? EventInternalTag { get; set; }
        public long? ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
