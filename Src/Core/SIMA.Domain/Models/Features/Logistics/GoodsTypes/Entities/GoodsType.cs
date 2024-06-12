using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.GoodsTypes.Entities;

public class GoodsType : Entity, IAggregateRoot
{
    private GoodsType() { }
    private GoodsType(CreateGoodsTypeArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        IsRequireItConfirmation = arg.IsRequireItConfirmation;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public async static Task<GoodsType> Create(CreateGoodsTypeArg arg, IGoodsTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new GoodsType(arg);
    }
    public async Task Modify(ModifyGoodsTypeArg arg, IGoodsTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        IsRequireItConfirmation = arg.IsRequireItConfirmation;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateGoodsTypeArg arg, IGoodsTypeDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyGoodsTypeArg arg, IGoodsTypeDomainService service)
    {

    }
    #endregion
    public GoodsTypeId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public string? IsRequireItConfirmation { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<GoodsCategory> _goodsCategories = new();
    public ICollection<GoodsCategory> GoodsCategories => _goodsCategories;
}