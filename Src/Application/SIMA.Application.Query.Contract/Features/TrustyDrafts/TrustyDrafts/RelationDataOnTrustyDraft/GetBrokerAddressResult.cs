namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts.RelationDataOnTrustyDraft
{
    public class GetBrokerAddressResult
    {
        public long? AddressTypeId { get; set; }
        public long? BrokerAddressListId { get; set; }
        public string? AddressTypeName { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
    }
}
