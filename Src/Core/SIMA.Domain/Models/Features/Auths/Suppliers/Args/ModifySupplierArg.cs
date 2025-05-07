namespace SIMA.Domain.Models.Features.Auths.Suppliers.Args;

public class ModifySupplierArg
{
    public long Id { get; set; }
    public long SupplierRankId { get; set; }
    public string? IsInBlackList { get; set; }
    public int SuccessOrderCountinTheYear { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? NationalCode { get; set; }
    public string? NationalId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
