using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.Reconsilations.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;

public class Broker : Entity
{
    private Broker() { }
    private Broker(CreateBrokerArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
        ExpireDate = arg.ExpireDate;
        BrokerTypeId = new(arg.BrokerTypeId);
    }
    public async Task Modify(ModifyBrokerArg arg, IBrokerService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
        ExpireDate = arg.ExpireDate;
        BrokerTypeId = new(arg.BrokerTypeId);
    }
    public static async Task<Broker> Create(CreateBrokerArg arg, IBrokerService broker)
    {
        await CreateGuards(arg, broker);
        return new Broker(arg);
    }

    #region Guards
    private static async Task CreateGuards(CreateBrokerArg arg, IBrokerService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await service.IsCodeUnique(arg.Code, 0))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyBrokerArg arg, IBrokerService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await service.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    #endregion
    #region AddMethods
    public void AddBrokerAccountBooks(List<CreateBrokerAccountBookArg> args)
    {
        foreach (var arg in args)
        {
            var entity = BrokerAccountBook.Create(arg);
            _brokerAccountBooks.Add(entity);
        }
    }
    public void AddBrokerPhoneBooks(List<CreateBrokerPhoneBookArg> args)
    {
        foreach (var arg in args)
        {
            var entity = BrokerPhoneBook.Create(arg);
            _brokerPhoneBooks.Add(entity);
        }
    }
    public void AddBrokerAddressBooks(List<CreateBrokerAddressBookArg> args)
    {
        foreach (var arg in args)
        {
            var entity = BrokerAddressBook.Create(arg);
            _brokerAddressBooks.Add(entity);
        }
    }
    #endregion
    #region ModifyMethods
    public void ModifyBrokerAccountBooks(List<CreateBrokerAccountBookArg> request , long brokerId)
    {


        brokerId.NullCheck();

        var previousEntity = _brokerAccountBooks.Where(x => x.BrokerId == new BrokerId(brokerId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addEntity = request.Where(x => !previousEntity.Any(c => c.IBANNumber == x.IbanNumber)).ToList();
        var deleteEntity = previousEntity.Where(x => !request.Any(c => c.IbanNumber == x.IBANNumber)).ToList();


        foreach (var item in addEntity)
        {
            var entity = _brokerAccountBooks.Where(x => (x.IBANNumber == item.IbanNumber && x.BrokerId == new BrokerId(brokerId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.Active((long)request[0].CreatedBy);
            }
            else
            {
                entity = BrokerAccountBook.Create(item);
                _brokerAccountBooks.Add(entity);
            }
        }

        foreach (var permission in deleteEntity)
        {
            permission.Delete((long)request[0].CreatedBy);
        }
    }
    public void ModifyBrokerPhoneBooks(List<CreateBrokerPhoneBookArg> request , long brokerId)
    {
        brokerId.NullCheck();

        var previousEntity = _brokerPhoneBooks.Where(x => x.BrokerId == new BrokerId(brokerId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addEntity = request.Where(x => !previousEntity.Any(c => c.PhoneNumber == x.PhoneNumber)).ToList();
        var deleteEntity = previousEntity.Where(x => !request.Any(c => c.PhoneNumber == x.PhoneNumber)).ToList();


        foreach (var item in addEntity)
        {
            var entity = _brokerPhoneBooks.Where(x => (x.PhoneNumber == item.PhoneNumber && x.BrokerId == new BrokerId(brokerId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.Active((long)request[0].CreatedBy);
            }
            else
            {
                entity = BrokerPhoneBook.Create(item);
                _brokerPhoneBooks.Add(entity);
            }
        }

        foreach (var permission in deleteEntity)
        {
            permission.Delete((long)request[0].CreatedBy);
        }
        
    }
    public void ModifyBrokerAddressBooks(List<CreateBrokerAddressBookArg> request , long brokerId)
    {
        brokerId.NullCheck();

        var previousEntity = _brokerAddressBooks.Where(x => x.BrokerId == new BrokerId(brokerId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addEntity = request.Where(x => !previousEntity.Any(c => c.Address == x.Address)).ToList();
        var deleteEntity = previousEntity.Where(x => !request.Any(c => c.Address == x.Address)).ToList();


        foreach (var item in addEntity)
        {
            var entity = _brokerAddressBooks.Where(x => (x.Address == item.Address && x.BrokerId == new BrokerId(brokerId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.Active((long)request[0].CreatedBy);
            }
            else
            {
                entity = BrokerAddressBook.Create(item);
                _brokerAddressBooks.Add(entity);
            }
        }

        foreach (var permission in deleteEntity)
        {
            permission.Delete((long)request[0].CreatedBy);
        }
        
    }
    #endregion
    #region DeleteMethods
    public void DeleteBrokerAccountBooks(long userId)
    {
        foreach (var item in _brokerAccountBooks)
        {
            item.Delete(userId);
        }
    }
    public void DeleteBrokerPhoneBooks(long userId)
    {
        foreach (var item in _brokerPhoneBooks)
        {
            item.Delete(userId);
        }
    }
    public void DeleteBrokerAddressBooks(long userId)
    {
        foreach (var item in _brokerAddressBooks)
        {
            item.Delete(userId);
        }
    }
    #endregion
    public BrokerId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public BrokerTypeId BrokerTypeId { get; private set; }
    public virtual BrokerType BrokerType { get; private set; }
    public DateTime? ExpireDate { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<BrokerAccountBook> _brokerAccountBooks = new();
    public ICollection<BrokerAccountBook> BrokerAccountBooks => _brokerAccountBooks;

    private List<BrokerPhoneBook> _brokerPhoneBooks = new();
    public ICollection<BrokerPhoneBook> BrokerPhoneBooks => _brokerPhoneBooks;

    private List<BrokerAddressBook> _brokerAddressBooks = new();
    public ICollection<BrokerAddressBook> BrokerAddressBooks => _brokerAddressBooks;

    //private List<Reconsilation> _reconsilations = new();
    //public ICollection<Reconsilation> Reconsilations => _reconsilations;

    private List<ReferralLetter> _referralLetters = new();
    public ICollection<ReferralLetter> ReferralLetters => _referralLetters;

    private List<TrustyDraft> _trustyDrafts = new();
    public ICollection<TrustyDraft> TrustyDrafts => _trustyDrafts;

    private List<Resource> _resources = new();
    public ICollection<Resource> Resources => _resources;

    private List<InquiryResponse> _inquiryResponses = new();
    public ICollection<InquiryResponse> InquiryResponses => _inquiryResponses;


    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        #region DeleteRelatedEntities
        DeleteBrokerAccountBooks(userId);
        DeleteBrokerAddressBooks(userId);
        DeleteBrokerPhoneBooks(userId);
        #endregion
    }
}
