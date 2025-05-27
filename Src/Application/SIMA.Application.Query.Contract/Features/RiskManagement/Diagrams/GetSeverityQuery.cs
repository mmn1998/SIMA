using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;

public class GetSeverityQuery : IQuery<Result<GetSeverityQueryResult>>
{
    public long RiskId { get; set; }
}


public class GetSeverityQueryResult
{
    public List<severityChart> severityChart { get; set; }
}

public class severityChart
{
    public string? id { get; set; }
    public List<SeverityAffectedHistoryValueTitle> Data { get; set; }

}

public class SeverityAffectedHistoryValueTitle
{
    public string? x { get; set; }
    public string? y { get; set; }
    public int isSelected { get; set; }
    public string Color { get; set; }
    public double ahNumericValue { get; set; }
}