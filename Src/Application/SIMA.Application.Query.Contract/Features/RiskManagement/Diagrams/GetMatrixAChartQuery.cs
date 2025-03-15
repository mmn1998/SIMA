using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;

public class GetMatrixAChartQuery : IQuery<Result<GetMatrixAChartQueryResult>>
{
    public long RiskId { get; set; }
}

public class GetMatrixAChartQueryResult
{
    public List<MatrixAChart> MatrixAChart { get; set; }
    
}

public class MatrixAChart
{
    public string? id { get; set; }
    public List<MatrixATriggerStatusValueTitle> Data { get; set; }

}


public class MatrixATriggerStatusValueTitle
{
    public string? x { get; set; }
    public string? y { get; set; }
    public int isSelected { get; set; }
    public string? Color { get; set; }
}