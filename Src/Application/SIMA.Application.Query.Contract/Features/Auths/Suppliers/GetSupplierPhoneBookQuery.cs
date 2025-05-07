namespace SIMA.Application.Query.Contract.Features.Auths.Suppliers
{
    public class GetSupplierPhoneBookQuery
    {
        public long PhoneBookId { get; set; }
        public long PhoneTypeId { get; set; }
        public string PhoneTypeName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
