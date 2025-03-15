namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;

public class ModifyDataProcedureArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string VersionNumber { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string Description { get; set; }
    public string? IsInternalApi { get; set; }
    public long DataBaseId { get; set; }
    public long DataProcedureTypeId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}