using SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;

public class BrokerAccountBook : Entity
{
    private BrokerAccountBook()
    {
        
    }
    private BrokerAccountBook(CreateBrokerAccountBookArg arg)
    {
        Id = new(arg.Id);
        BrokerId = new(arg.BrokerId);
        IBANNumber = arg.IbanNumber;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static BrokerAccountBook Create(CreateBrokerAccountBookArg arg)
    {
        return new BrokerAccountBook(arg);
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
    public BrokerAccountBookId Id { get; set; }
    public BrokerId BrokerId { get; set; }
    public virtual Broker Broker { get; set; }
    public string IBANNumber { get; set; }
    public DateTime? ActivityExpireDate { get; set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<BrokerAddressInfo> _brokerAddressInfos = new();
    public ICollection<BrokerAddressInfo> BrokerAddressInfos => _brokerAddressInfos;

}
