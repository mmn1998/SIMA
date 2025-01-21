namespace SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Args;

public class CreateLoanTypeArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
