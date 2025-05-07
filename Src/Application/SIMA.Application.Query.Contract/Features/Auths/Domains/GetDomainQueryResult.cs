namespace SIMA.Application.Query.Contract.Features.Auths.Domains;

public class GetDomainQueryResult
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
}
