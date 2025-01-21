namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts.RelationDataOnTrustyDraft
{
    public class GetBrokerPhoneResult
    {
        public long? PhoneTypeId { get; set; }
        public long? BrokerPhoneListId { get; set; }
        public string? PhoneTypeName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
