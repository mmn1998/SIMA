namespace SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAValues;

public class GetMatrixAValueQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public float? NumericValue { get; set; }
    public string? ValueTitle { get; set; }
    public string? Color { get; set; }
    public string? ActiveStatus { get; set; }
}