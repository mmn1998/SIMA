using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Enitites;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Entities;

public class WageRate : Entity
{
    private WageRate()
    {

    }
    private WageRate(CreateWageRateArg arg)
    {
        Id = new(arg.Id);
        CurrencyOperationTypeId = new(arg.CurrencyOperationTypeId);
        CurrencyTypeId = new(arg.CurrencyTypeId);
        PaymentTypeId = new(arg.PaymentTypeId);
        DraftTypeId = new(arg.DraftTypeId);
        CurrencyPaymentChannelId = new(arg.CurrencyPaymentChannelId);
        IsBasedOnPercentage = arg.IsBasedOnPercentage;
        Discount = arg.Discount;
        WageFixedValue = arg.WageFixedValue;
        WagePercentage = arg.WagePercentage;
        Name = arg.Name;
        Description = arg.Description;
        DraftOriginId = arg.DraftOriginId > 0 ? new(arg.DraftOriginId) :  null;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static WageRate Create(CreateWageRateArg arg)
    {
        CreateGuards(arg);
        return new WageRate(arg);
    }
    public void Modify(ModifyWageRateArg arg)
    {
        ModifyGuards(arg);
        CurrencyOperationTypeId = new(arg.CurrencyOperationTypeId);
        CurrencyTypeId = new(arg.CurrencyTypeId);
        PaymentTypeId = new(arg.PaymentTypeId);
        DraftTypeId = new(arg.DraftTypeId);
        CurrencyPaymentChannelId = new(arg.CurrencyPaymentChannelId);
        IsBasedOnPercentage = arg.IsBasedOnPercentage;
        Discount = arg.Discount;
        DraftOriginId = arg.DraftOriginId > 0 ? new(arg.DraftOriginId) : null;
        WageFixedValue = arg.WageFixedValue;
        WagePercentage = arg.WagePercentage;
        Name = arg.Name;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static void CreateGuards(CreateWageRateArg arg)
    {
        arg.IsBasedOnPercentage.NullCheck();
        if (string.Equals(arg.IsBasedOnPercentage, "1") && !arg.WagePercentage.HasValue)
            throw SimaResultException.NullException;
        if (string.Equals(arg.IsBasedOnPercentage, "0") && !arg.WageFixedValue.HasValue)
            throw SimaResultException.NullException;
        if (arg.WagePercentage < 0.01f || arg.WagePercentage > 100f)
            throw new SimaResultException(CodeMessges._100104Code, Messages.WagePercentageError);
        //if (arg.Discount == 0) throw SimaResultException.NullException;
        if (arg.WagePercentage == 0) throw SimaResultException.NullException;
        if (arg.WageFixedValue == 0) throw SimaResultException.NullException;
    }
    private void ModifyGuards(ModifyWageRateArg arg)
    {
        arg.IsBasedOnPercentage.NullCheck();
        if (string.Equals(arg.IsBasedOnPercentage, "1") && !arg.WagePercentage.HasValue)
            throw SimaResultException.NullException;
        if (string.Equals(arg.IsBasedOnPercentage, "0") && !arg.WageFixedValue.HasValue)
            throw SimaResultException.NullException;
        if (arg.WagePercentage < 0.01f || arg.WagePercentage > 100f)
            throw new SimaResultException(CodeMessges._100104Code, Messages.WagePercentageError);
        //if (arg.Discount == 0) throw SimaResultException.NullException;
        if (arg.WagePercentage == 0) throw SimaResultException.NullException;
        if (arg.WageFixedValue == 0) throw SimaResultException.NullException;
    }
    #endregion
    public WageRateId Id { get; private set; }
    public CurrencyOprationTypeId CurrencyOperationTypeId { get; private set; }
    public virtual CurrencyOprationType CurrencyOperationType { get; private set; }
    public CurrencyTypeId CurrencyTypeId { get; private set; }
    public virtual CurrencyType CurrencyType { get; private set; }
    public PaymentTypeId PaymentTypeId { get; private set; }
    public virtual PaymentType PaymentType { get; private set; }
    public DraftTypeId DraftTypeId { get; private set; }
    public virtual DraftType DraftType { get; private set; }
    public DraftOriginId? DraftOriginId { get; private set; }
    public virtual DraftOrigin? DraftOrigin { get; private set; }
    public CurrencyPaymentChannelId CurrencyPaymentChannelId { get; private set; }
    public virtual CurrencyPaymentChannel CurrencyPaymentChannel { get; private set; }
    public string Name { get; private set; }
    public string IsBasedOnPercentage { get; private set; }
    public decimal Discount { get; private set; }
    public float? WagePercentage { get; private set; }
    public decimal? WageFixedValue { get; private set; }
    public string? Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<InquiryResponse> _inquiryResponses = new();
    public ICollection<InquiryResponse> InquiryResponses => _inquiryResponses;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
