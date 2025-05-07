using SIMA.Domain.Models.Features.Auths.PhoneTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;

public class BrokerPhoneBook : Entity
{
    private BrokerPhoneBook()
    {

    }
    private BrokerPhoneBook(CreateBrokerPhoneBookArg arg)
    {
        Id = new(arg.Id);
        BrokerId = new(arg.BrokerId);
        PhoneTypeId = new(arg.PhoneTypeId);
        PhoneNumber = arg.PhoneNumber;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static BrokerPhoneBook Create(CreateBrokerPhoneBookArg arg)
    {
        return new BrokerPhoneBook(arg);
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
    public BrokerPhoneBookId Id { get; private set; }
    public BrokerId BrokerId { get; private set; }
    public virtual Broker Broker { get; private set; }
    public PhoneTypeId PhoneTypeId { get; private set; }
    public virtual PhoneType PhoneType { get; private set; }
    public string PhoneNumber { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<BrokerAddressInfo> _brokerAddressInfos = new();
    public ICollection<BrokerAddressInfo> BrokerAddressInfos => _brokerAddressInfos;

    
}
