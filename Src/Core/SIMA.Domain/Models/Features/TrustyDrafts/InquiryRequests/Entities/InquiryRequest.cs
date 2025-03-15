using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Customers.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.ValueObjects;
using SIMA.Domain.Models.Features.Notifications.Messages.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Exceptions;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;


public class InquiryRequest : Entity
{
    private InquiryRequest()
    {

    }
    private InquiryRequest(CreateInquiryRequestArg arg, IInquiryRequestDomainService service)
    {
        Id = new(arg.Id);
        PaymentTypeId = new(arg.PaymentTypeId);
        CustomerId = new(arg.CustomerId);
        BeneficiaryName = arg.BeneficiaryName;
        ReferenceNumber = arg.ReferenceNumber;
        DraftOrderNumber = arg.DraftOrderNumber;
        ProformaNumber = arg.ProformaNumber;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        BranchId = new(service.GetBranchIdByUserId(arg.CreatedBy).GetAwaiter().GetResult());
        DraftOriginId = new(arg.DraftOriginId);
        ProformaAmount = arg.ProformaAmount;
        DraftOrderDate = arg.DraftOrderDate;
        ProformaDate = arg.ProformaDate;
        ProformaCurrencyTypeId = new(arg.ProformaCurrencyTypeId);
    }
    public static async Task<InquiryRequest> Create(CreateInquiryRequestArg arg, IInquiryRequestDomainService service)
    {
        await CreateGuards(arg, service);
        return new InquiryRequest(arg, service);
    }
    #region Guards
    private static async Task CreateGuards(CreateInquiryRequestArg arg, IInquiryRequestDomainService service)
    {
        if (arg.ProformaCurrencyTypeId == (long)PaymentTypeEnum.Deposit)
            if (string.IsNullOrEmpty(arg.ProformaNumber)) throw InquiryRequestExceptions.DepositProformaCurrencyTypeIdException;

        if (await service.CheckRefrenceNumber(arg.ReferenceNumber))
            throw new  SimaResultException(CodeMessges._400Code, Messages.ReferenceNumberIsDuplicated);

    }
    #endregion
    public async Task Modify(ModifyInquiryRequestArg arg, IInquiryRequestDomainService service)
    {
        PaymentTypeId = new(arg.PaymentTypeId);
        CustomerId = new(arg.CustomerId);
        BeneficiaryName = arg.BeneficiaryName;
        ReferenceNumber = arg.ReferenceNumber;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        BranchId = new(await service.GetBranchIdByUserId(arg.ModifiedBy));
    }
    public InquiryRequestId Id { get; private set; }
    public string BeneficiaryName { get; private set; }
    public string ReferenceNumber { get; private set; }
    public string? DraftOrderNumber { get; private set; }
    public DateTime? DraftOrderDate { get; private set; }
    public DateTime? ProformaDate { get; private set; }
    public decimal? ProformaAmount { get; private set; }
    public string? ProformaNumber { get; private set; }
    public string? Description { get; private set; }
    public PaymentTypeId PaymentTypeId { get; private set; }
    public virtual PaymentType PaymentType { get; private set; }
    public CurrencyTypeId? ProformaCurrencyTypeId { get; private set; }
    public virtual CurrencyType? ProformaCurrencyType { get; private set; }
    public DraftOriginId? DraftOriginId { get; private set; }
    public virtual DraftOrigin? DraftOrigin { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public virtual Customer Customer { get; private set; }
    public BranchId BranchId { get; private set; }
    public virtual Branch Branch { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<InquiryResponse> _inquiryResponses = new();
    public ICollection<InquiryResponse> InquiryResponses => _inquiryResponses;

    private List<TrustyDraft> _trustyDrafts = new();
    public ICollection<TrustyDraft> TrustyDrafts => _trustyDrafts;

    private List<InquiryRequestCurrency> _inquiryRequestCurrencies = new();
    public ICollection<InquiryRequestCurrency> InquiryRequestCurrencies => _inquiryRequestCurrencies;
    private List<InquiryRequestDocument> _inquiryRequestDocuments = new();
    public ICollection<InquiryRequestDocument> InquiryRequestDocuments => _inquiryRequestDocuments;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void AddDocuments(List<CreateInquiryRequestDocumentArg> args)
    {
        foreach (var arg in args)
        {
            var entity = InquiryRequestDocument.Create(arg);
            _inquiryRequestDocuments.Add(entity);
        }
    }
    public void AddCurrencies(List<CreateInquiryRequestCurrencyArg> args)
    {
        foreach (var arg in args)
        {
            var entity = InquiryRequestCurrency.Create(arg);
            _inquiryRequestCurrencies.Add(entity);
        }
    }
}
