namespace SIMA.Application.Query.Contract.Features.Auths.Suppliers
{
    public class GetSupplierAddressBookQuery
    {
        public long AddressBookId { get; set; }
        public long AddressTypeId { get; set; }
        public string AddressTypeName { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
    }
}
