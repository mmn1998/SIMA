using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Entities;

public class GoodsQuorumPrice : Entity, IAggregateRoot
{
    private GoodsQuorumPrice() { }
    private GoodsQuorumPrice(CreateGoodsQuorumPriceArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        IsRequiredCeoConfirmation = arg.IsRequiredCeoConfirmation;
        IsRequiredSupplierWrittenInquiry = arg.IsRequiredSupplierWrittenInquiry;
        IsRequiredBoardConfirmation = arg.IsRequiredBoardConfirmation;
        MaxPrice = arg.MaxPrice;
        MinPrice = arg.MinPrice;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<GoodsQuorumPrice> Create(CreateGoodsQuorumPriceArg arg, IGoodsQuorumPriceDomainService service)
    {
        await CreateGuards(arg, service);
        return new GoodsQuorumPrice(arg);
    }
    public async Task Modify(ModifyGoodsQuorumPriceArg arg, IGoodsQuorumPriceDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        IsRequiredCeoConfirmation = arg.IsRequiredCeoConfirmation;
        IsRequiredSupplierWrittenInquiry = arg.IsRequiredSupplierWrittenInquiry;
        IsRequiredBoardConfirmation = arg.IsRequiredBoardConfirmation;
        MaxPrice = arg.MaxPrice;
        MinPrice = arg.MinPrice;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateGoodsQuorumPriceArg arg, IGoodsQuorumPriceDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyGoodsQuorumPriceArg arg, IGoodsQuorumPriceDomainService service)
    {

    }
    #endregion
    public GoodsQuorumPriceId Id { get; private set; }
    public string? IsRequiredCeoConfirmation { get; private set; }
    public string? IsRequiredBoardConfirmation { get; private set; }
    public string? IsRequiredSupplierWrittenInquiry { get; private set; }
    public float MinPrice { get; private set; }
    public float MaxPrice { get; private set; }
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
