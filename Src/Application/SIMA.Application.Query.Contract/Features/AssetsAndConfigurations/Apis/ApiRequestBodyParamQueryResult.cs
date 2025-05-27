namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;

public class ApiRequestBodyParamQueryResult
{
    public string Name { get; set; }
    public string DataType { get; set; }
    public string IsMandatory { get; set; }
    public long ParentId { get; set; }
    public string Parent { get; set; }
    public string Description { get; set; }
}
