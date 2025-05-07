namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetFormUserQuery 
{
    public long DomainId { get; set; }
    public long FormId { get; set; }
    public string? DomainName { get; set; }
    public string? FormName { get; set; }
    public string? FormTitle { get; set; }
}

