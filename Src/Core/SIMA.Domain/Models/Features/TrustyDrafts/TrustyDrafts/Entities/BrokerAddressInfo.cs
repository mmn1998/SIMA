using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities
{
    public class BrokerAddressInfo : Entity
    {
        public BrokerAddressInfoId Id { get; private set; }
        public TrustyDraftId TrustyDraftId { get; private set; }
        public virtual TrustyDraft TrustyDraft { get; private set; }
        public BrokerAddressBookId BrokerAddressBookId { get; private set; }
        public virtual BrokerAddressBook BrokerAddressBook { get; private set; }
        public BrokerPhoneBookId BrokerPhoneBookId { get; private set; }
        public virtual BrokerPhoneBook BrokerPhoneBook { get; private set; }
        public BrokerAccountBookId BrokerAccountBookId { get; private set; }
        public virtual BrokerAccountBook BrokerAccountBook { get; private set; }
        public string IsLastConfirmedAddress { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

    }
}
