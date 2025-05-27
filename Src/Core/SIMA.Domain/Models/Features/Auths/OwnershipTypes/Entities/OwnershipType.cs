using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Args;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.OwnershipTypes.Entities;

public class OwnershipType : Entity, IAggregateRoot
{
    private OwnershipType()
    {
        
    }
    private OwnershipType(CreateOwnershipTypeArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<OwnershipType> Create(CreateOwnershipTypeArg arg, IOwnershipTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new OwnershipType(arg);
    }
    public async Task Modify(ModifyOwnershipTypeArg arg, IOwnershipTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateOwnershipTypeArg arg, IOwnershipTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyOwnershipTypeArg arg, IOwnershipTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public OwnershipTypeId Id { get; private set; }
    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<Asset> _assets = new();
    public ICollection<Asset> Assets => _assets;
}