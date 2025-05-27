using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Entities;

public class BusinessCriticality : Entity
{
    private BusinessCriticality() { }
    private BusinessCriticality(CreateBusinessCriticalityArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        Ordering = arg.Ordering;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<BusinessCriticality> Create(CreateBusinessCriticalityArg arg, IBusinessCriticalityDomainService service)
    {
        await CreateGuards(arg, service);
        return new BusinessCriticality(arg);
    }
    public async Task Modify(ModifyBusinessCriticalityArg arg, IBusinessCriticalityDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        Ordering = arg.Ordering;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateBusinessCriticalityArg arg, IBusinessCriticalityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (arg.Ordering != null)
        {
             if (!await service.IsOrderingUnique(arg.Ordering)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueOrderingError);
        }
        
    }
    private async Task ModifyGuards(ModifyBusinessCriticalityArg arg, IBusinessCriticalityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (arg.Ordering != null)
        {
            if (await service.IsOrderingUnique(arg.Ordering,Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueOrderingError);
        }
    }
    #endregion
    public BusinessCriticalityId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public float? Ordering { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<Asset> _assets = new();
    public ICollection<Asset> Assets => _assets;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<ConfigurationItem> _configurationItems = new();
    public ICollection<ConfigurationItem> ConfigurationItems => _configurationItems;
}
