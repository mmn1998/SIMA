namespace SIMA.Application.Query.Contract.Features.BCP.ConsequenceValues;

public class GetConsequenceValueQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long OriginId { get; set; }
    public string? OriginName { get; set; }
    public long ConsequenceId { get; set; }
    public string? ConsequenceName { get; set; }
    public float ValueNumber { get; set; }
    public string? ActiveStatus { get; set; }
}