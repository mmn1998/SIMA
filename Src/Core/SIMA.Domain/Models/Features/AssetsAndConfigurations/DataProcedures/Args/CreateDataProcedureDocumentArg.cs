namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;

public class CreateDataProcedureDocumentArg
{
    public long Id { get; set; }
    public long DataProcedureId { get; set; }
    public long DocumentId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
