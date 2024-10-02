namespace SIMA.Domain.Models.Features.Auths.Suppliers.Args
{
    public class CreateSupplierAddressBookArg
    {
        public long Id { get;  set; }
        public long SupplierId { get;  set; }
        public long AddressTypeId { get;  set; }
        public string PostalCode { get;  set; }
        public string Address { get;  set; }
        public long ActiveStatusId { get;  set; }
        public DateTime? CreatedAt { get;  set; }
        public long? CreatedBy { get;  set; }
    }
}
