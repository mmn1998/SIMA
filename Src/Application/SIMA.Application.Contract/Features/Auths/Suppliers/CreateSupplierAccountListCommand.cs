namespace SIMA.Application.Contract.Features.Auths.Suppliers
{
    public class CreateSupplierAccountListCommand
    {
        public long Id { get; set; }
        public string IBAN { get; set; }
    }
}
