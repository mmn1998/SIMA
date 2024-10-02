namespace SIMA.Application.Contract.Features.Auths.Suppliers
{
    public class CreateSupplierAddressBookCommand
    {
        public long Id { get; set; }
        public long AddressTypeId { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
    }
}
