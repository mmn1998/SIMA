using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

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
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        if (arg.MaxPrice < arg.MinPrice) throw new SimaResultException(CodeMessges._400Code, Messages.MinNumberBiggerThanMaxNumber);
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyGoodsQuorumPriceArg arg, IGoodsQuorumPriceDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        if (arg.MaxPrice < arg.MinPrice) throw new SimaResultException(CodeMessges._400Code, Messages.MinNumberBiggerThanMaxNumber);
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public GoodsQuorumPriceId Id { get; private set; }
    public string? IsRequiredCeoConfirmation { get; private set; }
    public string? IsRequiredBoardConfirmation { get; private set; }
    public string? IsRequiredSupplierWrittenInquiry { get; private set; }
    public decimal MinPrice { get; private set; }
    public decimal MaxPrice { get; private set; }
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
}
