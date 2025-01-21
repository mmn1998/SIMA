using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AddressTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;

public class BrokerAddressBook : Entity
{
    private BrokerAddressBook()
    {

    }
    private BrokerAddressBook(CreateBrokerAddressBookArg arg)
    {
        Id = new(arg.Id);
        BrokerId = new(arg.BrokerId);
        AddressTypeId = new(arg.AddressTypeId);
        Address = arg.Address;
        PostalCode = arg.PostalCode;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static BrokerAddressBook Create(CreateBrokerAddressBookArg arg)
    {
        return new BrokerAddressBook(arg);
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
    public BrokerAddressBookId Id { get; private set; }
    public BrokerId BrokerId { get; private set; }
    public virtual Broker Broker { get; private set; }
    public AddressTypeId AddressTypeId { get; private set; }
    public virtual AddressType AddressType { get; private set; }
    public string Address { get; private set; }
    public string PostalCode { get; private set; }
    public DateTime? ActivityExpireDate { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<BrokerAddressInfo> _brokerAddressInfos = new();
    public ICollection<BrokerAddressInfo> BrokerAddressInfos => _brokerAddressInfos;

    
}
