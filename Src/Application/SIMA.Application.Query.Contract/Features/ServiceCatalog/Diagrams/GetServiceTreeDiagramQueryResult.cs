namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetServiceTreeDiagramQueryResult
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Color { get; set; }
    public long RecordId { get; set; }
    public List<GetServiceTreeDiagramQueryResult>? Children { get; set; }
}public class GetServiceTreeDiagramQueryResultWrapper
{
    public List<GetServiceTreeDiagramQueryResult>? data { get; set; }
}