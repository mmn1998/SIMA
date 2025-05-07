namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues
{
    public class GetServiceInputParam
    {
        public long ServiceInputId { get; set; }
        public string DataTypeName { get; set; }
        public string ServiceInputName { get; set; }
    }
}
