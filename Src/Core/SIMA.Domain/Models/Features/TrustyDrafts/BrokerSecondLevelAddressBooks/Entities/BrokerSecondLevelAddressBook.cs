using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Entities;

public class BrokerSecondLevelAddressBook : Entity, IAggregateRoot
{
    private BrokerSecondLevelAddressBook()
    {

    }
    private BrokerSecondLevelAddressBook(CreateBrokerSecondLevelAddressBookArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Address = arg.Address;
        PhoneNumber = arg.PhoneNumber;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BrokerSecondLevelAddressBook Create(CreateBrokerSecondLevelAddressBookArg arg, IBrokerSecondLevelAddressBookDomainService service)
    {
        CreateGuards(arg, service);
        return new BrokerSecondLevelAddressBook(arg);
    }
    #region Guards
    private static void CreateGuards(CreateBrokerSecondLevelAddressBookArg arg, IBrokerSecondLevelAddressBookDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    private void ModifyGuards(ModifyBrokerSecondLevelAddressBookArg arg, IBrokerSecondLevelAddressBookDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    #endregion
    public void Modify(ModifyBrokerSecondLevelAddressBookArg arg, IBrokerSecondLevelAddressBookDomainService service)
    {
        ModifyGuards(arg, service);
        Name = arg.Name;
        Address = arg.Address;
        PhoneNumber = arg.PhoneNumber;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public BrokerSecondLevelAddressBookId Id { get; private set; }
    public string Name { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Address { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<TrustyDraft> _trustyDrafts = new();
    public ICollection<TrustyDraft> TrustyDrafts => _trustyDrafts;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}