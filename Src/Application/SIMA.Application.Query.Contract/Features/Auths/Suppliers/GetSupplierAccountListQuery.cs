namespace SIMA.Application.Query.Contract.Features.Auths.Suppliers
{
    public class GetSupplierAccountListQuery
    {
        public long AccountListId { get; set; }
        public string IBAN { get; set; }
    }
}
