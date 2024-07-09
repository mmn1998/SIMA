namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues
{
    public class StoreProcedureParams
    {
        //public long Id { get; set; }
        public long ProgressStoredProcedureParamId { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        //public long DataTypeId { get; set; }
        //public bool IsRequired { get; set; }
        //public bool ActiveStatusId { get; set; }
    }

}
