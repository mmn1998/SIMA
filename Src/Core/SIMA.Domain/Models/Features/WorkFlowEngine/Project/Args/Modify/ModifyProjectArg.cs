namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Modify;

public class ModifyProjectArg
{
    public int? DomainId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}
