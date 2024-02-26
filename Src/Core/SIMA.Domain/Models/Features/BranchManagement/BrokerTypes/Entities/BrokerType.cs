using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Entities;

public class BrokerType : Entity
{
    private BrokerType()
    {

    }
    private BrokerType(CreateBrokerTypeArg arg)
    {
        Id = new BrokerTypeId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<BrokerType> Create(CreateBrokerTypeArg arg, IBrokerTypeDomainService domainService)
    {
        await CreateGuards(arg, domainService);
        return new BrokerType(arg);
    }
    public async Task Modify(ModifyBrokerTypeArg arg, IBrokerTypeDomainService domainService)
    {
        await ModifyGuards(arg, domainService);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public async Task Deactive()
    {
        ActiveStatusId = 2;
    }
    public BrokerTypeId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public virtual ICollection<Broker> Brokers { get; private set; } = new List<Broker>();

    #region Guards
    private static async Task CreateGuards(CreateBrokerTypeArg arg, IBrokerTypeDomainService brokerTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw SimaResultException.LengthNameException;

        if (arg.Code.Length >= 20)
            throw SimaResultException.LengthCodeException;

        if (await brokerTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw SimaResultException.UniqueCodeError;
    }
    private async Task ModifyGuards(ModifyBrokerTypeArg arg, IBrokerTypeDomainService brokerTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw SimaResultException.LengthNameException;

        if (arg.Code.Length >= 20)
            throw SimaResultException.LengthCodeException;

        if (await brokerTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw SimaResultException.UniqueCodeError;
    }

    #endregion
}
