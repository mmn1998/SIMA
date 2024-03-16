using SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;

public class Broker : Entity
{
    private Broker() { }
    private Broker(CreateBrokerArg arg)
    {
        Id = new BrokerId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
        Address = arg.Address;
        PhoneNumber = arg.PhoneNumber;
        ExpireDate = arg.ExpireDate;
        if (arg.BrokerTypeId.HasValue) BrokerTypeId = new BrokerTypeId(arg.BrokerTypeId.Value);
    }
    public async Task Modify(ModifyBrokerArg arg, IBrokerService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
        Address = arg.Address;
        PhoneNumber = arg.PhoneNumber;
        ExpireDate = arg.ExpireDate;
        if (arg.BrokerTypeId.HasValue) BrokerTypeId = new BrokerTypeId(arg.BrokerTypeId.Value);
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
            throw SimaResultException.LengthNameException;

        if (arg.Code.Length >= 20)
            throw SimaResultException.LengthCodeException;

        if (await service.IsCodeUnique(arg.Code, 0))
            throw SimaResultException.UniqueCodeError;
    }
    private async Task ModifyGuards(ModifyBrokerArg arg, IBrokerService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw SimaResultException.LengthNameException;

        if (arg.Code.Length >= 20)
            throw SimaResultException.LengthCodeException;

        if (await service.IsCodeUnique(arg.Code, arg.Id))
            throw SimaResultException.UniqueCodeError;
    }
    #endregion
    public BrokerId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public BrokerTypeId? BrokerTypeId { get; private set; }

    public string? PhoneNumber { get; private set; }

    public string? Address { get; private set; }
    public DateTime? ExpireDate { get; private set; }

    public long? ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual BrokerType? BrokerType { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
