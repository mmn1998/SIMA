using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Args;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

namespace SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Entities;

public class ResponsibleType : Entity
{
    private ResponsibleType() { }
    private ResponsibleType(CreateResponsibleTypeArg arg)
    {
        Id = new ResponsibleTypeId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ResponsibleType> Create(CreateResponsibleTypeArg arg, IResponsibleTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new ResponsibleType(arg);
    }
    public async Task Modify(ModifyResponsibleTypeArg arg, IResponsibleTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        Name = arg.Name;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }

    #region Gaurds
    private static async Task CreateGuards(CreateResponsibleTypeArg arg, IResponsibleTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyResponsibleTypeArg arg, IResponsibleTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public ResponsibleTypeId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<ProductResponsible> _productResponsible = new();
    public ICollection<ProductResponsible> ProductResponsibles => _productResponsible;

    private List<ChannelResponsible> _channelResponsible = new();
    public ICollection<ChannelResponsible> ChannelResponsibles => _channelResponsible;
    private List<CriticalActivityAssignedStaff> _criticalActivityAssignedStaffs = new();
    public ICollection<CriticalActivityAssignedStaff> CriticalActivityAssignedStaffs => _criticalActivityAssignedStaffs;
    private List<ServiceAssignedStaff> _serviceAssignedStaffs = new();
    public ICollection<ServiceAssignedStaff> ServiceAssignedStaffs => _serviceAssignedStaffs;
    private List<RiskStaff> _riskStaves = new();
    public ICollection<RiskStaff> RiskStaves => _riskStaves;
    
    
    private List<AssetAssignedStaff> _assetAssignedStaffs = new();
    public ICollection<AssetAssignedStaff> AssetAssignedStaffs => _assetAssignedStaffs;
}
