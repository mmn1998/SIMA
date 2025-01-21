namespace SIMA.Domain.Models.Features.Auths.Suppliers.Args;

public class CreateSupplierArg
{
    public long Id { get; set; }
    public long SupplierRankId { get; set; }
    public string? IsInBlackList { get; set; }
    public int SuccessOrderCountinTheYear { get;  set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? NationalCode { get; set; }
    public string? NationalId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }

}