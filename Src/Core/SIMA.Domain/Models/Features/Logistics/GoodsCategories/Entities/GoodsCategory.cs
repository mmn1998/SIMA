using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;

public class GoodsCategory : Entity, IAggregateRoot
{
    private GoodsCategory() { }
    private GoodsCategory(CreateGoodsCategoryArg arg)
    {
        Id = new(arg.Id);
        GoodsTypeId = new(arg.GoodsTypeId);
        Name = arg.Name;
        Code = arg.Code;
        IsGoods = arg.IsGoods;
        IsHardware = arg.IsHardware;
        IsRequiredSecurityCheck = arg.IsRequiredSecurityCheck;
        IsTechnological = arg.IsTechnological;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public async static Task<GoodsCategory> Create(CreateGoodsCategoryArg arg, IGoodsCategoryDomainService service)
    {
        await CreateGuards(arg, service);
        return new GoodsCategory(arg);
    }
    public async Task Modify(ModifyGoodsCategoryArg arg, IGoodsCategoryDomainService service)
    {
        await ModifyGuards(arg, service);
        GoodsTypeId = new(arg.GoodsTypeId);
        Name = arg.Name;
        Code = arg.Code;
        IsGoods = arg.IsGoods;
        IsHardware = arg.IsHardware;
        IsRequiredSecurityCheck = arg.IsRequiredSecurityCheck;
        IsTechnological = arg.IsTechnological;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateGoodsCategoryArg arg, IGoodsCategoryDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyGoodsCategoryArg arg, IGoodsCategoryDomainService service)
    {

    }
    #endregion
    public GoodsCategoryId Id { get; private set; }
    public GoodsTypeId GoodsTypeId { get; private set; }
    public virtual GoodsType GoodsType { get; private set; }
    public string? IsTechnological { get; private set; }
    public string? IsHardware { get; private set; }
    public string? IsGoods { get; private set; }
    public string? IsRequiredSecurityCheck { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
