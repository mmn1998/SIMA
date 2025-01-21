using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Exceptions;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.Responses.Entities;

public class InquiryResponse : Entity
{
    private InquiryResponse()
    {

    }
    private InquiryResponse(CreateInquiryResponseArg arg)
    {
        Id = new(arg.Id);
        BrokerInquiryStatusId = new(arg.BrokerInquiryStatusId);
        InquiryRequestCurrencyId = new(arg.InquiryRequestCurrencyId);
        InquiryRequestId = new(arg.InquiryRequestId);
        BrokerId = new(arg.BrokerId);
        WageRateId = new(arg.WageRateId);
        CalculatedWage = arg.CalculatedWage;
        ExcessWage = arg.ExcessWage;
        ValidityPeriod = arg.ValidityPeriod;
        ActiveStatusId = arg.ActiveStatusId;
        Description = arg.Description;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<InquiryResponse> Create(CreateInquiryResponseArg arg, IInquiryResponseDomainService service)
    {
        await CreateGuards(arg, service);
        return new InquiryResponse(arg);
    }
    #region Guards
    private static async Task CreateGuards(CreateInquiryResponseArg arg, IInquiryResponseDomainService service)
    {
        arg.NullCheck();
        if (!await service.CurrencyTypeIdEquals(arg.WageRateId, arg.InquiryRequestCurrencyId)) throw InquiryResponseExceptions.InvalidWageRateException; 
    }
    //private async Task ModifyGuards(ModifyInquiryRequestArg arg, IInquiryRequestDomainService service)
    //{
    //    arg.NullCheck();
    //    arg.Name.NullCheck();
    //    arg.Code.NullCheck();

    //    if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    //    if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
    //    if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    //}
    #endregion
    public async Task Modify(ModifyInquiryResponseArg arg, IInquiryResponseDomainService service)
    {
        BrokerInquiryStatusId = new(arg.BrokerInquiryStatusId);
        InquiryRequestId = new(arg.InquiryRequestId);
        BrokerId = new(arg.BrokerId);
        WageRateId = new(arg.WageRateId);
        CalculatedWage = arg.CalculatedWage;
        ExcessWage = arg.ExcessWage;
        //ValidityPeriod = arg.ValidityPeriod;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public InquiryResponseId Id { get; private set; }
    public InquiryRequestId InquiryRequestId { get; private set; }
    public virtual InquiryRequest InquiryRequest { get; private set; }
    public BrokerInquiryStatusId BrokerInquiryStatusId { get; private set; }
    public virtual BrokerInquiryStatus BrokerInquiryStatus { get; private set; }
    public InquiryRequestCurrencyId InquiryRequestCurrencyId { get; private set; }
    public virtual InquiryRequestCurrency InquiryRequestCurrency { get; private set; }
    public BrokerId BrokerId { get; private set; }
    public virtual Broker Broker { get; private set; }
    public WageRateId WageRateId { get; private set; }
    public virtual WageRate WageRate { get; private set; }
    public decimal CalculatedWage { get; private set; }
    public decimal? ExcessWage { get; private set; }
    public string? Description { get; private set; }
    public DateTime? ValidityPeriod { get; private set; }
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
