namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow;

public class QueryWithCheckerModel
{
    public string Query { get; set; }
    public string Checker { get; set; }
    public string ValueToCheck { get; set; }
    public string Combiner { get; set; }
    public bool IsLong => long.TryParse(ValueToCheck, out var item);
}
