namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetServiceNetworkDiagramQueryResult
{
    public List<ServiceNetworkDiagramNode>? Nodes { get; set; }
    public List<List<string>>? Edges { get; set; }
    public List<Tags>? Tags { get; set; }
    public List<Clusters>? Clusters { get; set; }
}
public class GetServiceNetworkDiagramQueryResultWrapper
{
    public List<GetServiceNetworkDiagramQueryResult>? data { get; set; }
}


public class Tags
{
    public string? key { get; set; }
    public string? image { get; set; }
}
public class Clusters
{
    public string? key { get; set; }
    public string? color { get; set; }
    public string? clusterLabel { get; set; }
}