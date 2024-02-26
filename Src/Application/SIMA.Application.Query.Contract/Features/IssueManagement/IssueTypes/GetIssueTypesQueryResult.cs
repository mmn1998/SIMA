namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssueTypes;

public class GetIssueTypesQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string IconPath { get; set; }
    public string ColorHex { get; set; }
    public string? ActiveStatus { get; set; }
    public long ActiveStatusId { get; set; }
}
