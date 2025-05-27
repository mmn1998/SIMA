namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetChannelListDiagramResult
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Scope { get; set; }
    public string? Description { get; set; }
    public long ServiceStatusId { get; set; }
    public string? InServiceDate { get; set; }
    public List<ChannelAccessPoint>? ChannelAccessPoint { get; set; }
    public string? CreatedAt { get; set; }

}



public class GetChannelListResultWrapper
{
    public List<GetChannelListDiagramResult>? data { get; set; }
}