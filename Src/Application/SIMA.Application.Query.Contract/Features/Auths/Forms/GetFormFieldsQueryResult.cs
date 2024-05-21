namespace SIMA.Application.Query.Contract.Features.Auths.Forms;

public class GetFormFieldsQueryResult
{
    public long Id { get; set; }
    public long FormId { get; set; }
    public string? Code { get; set; }
    public string? Type { get; set; }
    public string? Name { get; set; }
    public string? FormName { get; set; }
    public string? ActiveStatus { get; set; }
    public long ActiveStatusId { get; set; }
}
