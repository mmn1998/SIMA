namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Apis;

public class CreateApiRequestBodyParam
{
    public string Name { get; set; }
    public string DataType{ get; set; }
    public string IsMandatory { get; set; }
    public long? ParentId { get; set; }
    public string Description { get; set; }
}
