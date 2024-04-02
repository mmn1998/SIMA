namespace SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.Args;

internal class ModifyResponsibleAnswerTypeArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
