namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;

public class CreateApiArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? Prerequisites { get; set; }
    public string? BaseUrl { get; set; }
    public string? ApiAddress { get; set; }
    public string? IpAddress { get; set; }
    public string? PortNumber { get; set; }
    public int? RateLimitingMin { get; set; }
    public int? RateLimitingMax { get; set; }
    public long? ApiTypeId { get; set; }
    public long? ApiMethodCallId { get; set; }
    public long? ApiAuthentoicationMethodId { get; set; }
    public long? NetworkProtocolId { get; set; }
    public string? AuthenticationWorkflow { get; set; }
    public long? OwnerDepartmentId { get; set; }
    public long? OwnerResponsibleId { get; set; }
    public string? RulesAndConditions { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}