namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args
{
    public class ProgressStoreProcedureArg
    {
        public long Id { get; set; }
        public long ProgressId { get; set; }
        public float ExecutionOrdering { get; set; }
        public string StoreProcedureName { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }

        public List<ProgressStoreProcedureParamArg> ProgressStoreProcedureParams { get; set; }
    }
}
