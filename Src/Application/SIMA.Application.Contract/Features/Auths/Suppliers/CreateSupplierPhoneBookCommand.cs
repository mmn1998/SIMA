namespace SIMA.Application.Contract.Features.Auths.Suppliers
{
    public class CreateSupplierPhoneBookCommand
    {
        public long Id { get; set; }
        public long PhoneTypeId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
