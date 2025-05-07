namespace SIMA.Domain.Models.Features.Auths.SupplierRanks.Args;

public class ModifySupplierRankArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float? Ordering { get; set; }
    public int SupplierSuccessOrderCountForm { get;  set; }
    public int SupplierSuccessOrderCountTo { get;  set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
