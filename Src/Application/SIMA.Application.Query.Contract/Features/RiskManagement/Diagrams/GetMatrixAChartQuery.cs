using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;

public class GetMatrixAChartQuery : IQuery<Result<GetMatrixAChartQueryResult>>
{
    public long MatrixAChartId { get; set; }
}

public class GetMatrixAChartQueryResult
{
    public long id { get; set; }
    public string? name { get; set; }
    public string? code { get; set; }
    public string? MatrixAId { get; set; }
    public string? MatrixAValueId { get; set; }
    public string? MatrixAValueValueTitle { get; set; }
    public string? MatrixAValueNumericValue { get; set; }
    public string? TriggerStatusId { get; set; }
    public string? TriggerStatusTitle { get; set; }
    public string? TriggerStatusValueTitle { get; set; }
    public string? TriggerStatusNumericValue { get; set; }
    public string? UseVulnerabilityId { get; set; }
    public string? UseVulnerabilityTitle { get; set; }
    public string? UseVulnerabilityValueTitle { get; set; }
    public string? UseVulnerabilityNumericValue { get; set; }
    public List<MatrixAChart> MatrixATriggerStatusValueTitle { get; set; }
    
}

public class MatrixAChart
{
    public string? id { get; set; }
    public List<MatrixATriggerStatusValueTitle> Data { get; set; }
    public string? isSelected { get; set; }
    public string? Color { get; set; }
}


public class MatrixATriggerStatusValueTitle
{
    public string? x { get; set; }
    public string? y { get; set; }
}