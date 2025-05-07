namespace SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.Args
{
    public class ModifyActionTypeArg
    {
        public string MainType { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? EventTag { get; set; }
        public string? EventInternalTag { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
