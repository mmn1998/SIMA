namespace SIMA.Domain.Models.Features.Auths.SupplierRanks.Args;

public class CreateSupplierRankArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float? Ordering { get; set; }
    public int SupplierSuccessOrderCountForm { get; set; }
    public int SupplierSuccessOrderCountTo { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}