#region usings
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Customers.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.Reconsilations.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Events;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;
#endregion

namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;

public class TrustyDraft : Entity
{
    private TrustyDraft() { }
    private TrustyDraft(CreateTrustyDraftArg arg)
    {
        Id = new(arg.Id);
        IssueId = arg.IssueId != 0 ? new(arg.IssueId) : null;
        BranchId = new(arg.BranchId);
        CustomerId = new(arg.CustomerId);
        ValorDate = arg.ValorDate;
        DraftNumber = arg.DraftNumber;
        ValorDate = arg.ValorDate;
        DraftNumber = arg.DraftNumber;
        DraftNumberBasedOnOrder = arg.DraftNumberBasedOnOrder;
        CustomerId = new CustomerId(arg.CustomerId);
        DraftOriginId = arg.DraftOriginId != 0 ? new(arg.DraftOriginId) : null;
        CustomerAccountNumber = arg.CustomerAccountNumber;
        BlockNumber = arg.BlockNumber;
        DraftIssueDate = arg.DraftIssueDate;
        BlockingNumber = arg.BlockingNumber;
        DraftRequestAmount = arg.DraftRequestAmount;
        DraftRequestAmountBasedOnUsd = arg.DraftRequestAmountBasedOnUsd;
        DraftRequestAmountBasedOnEur = arg.DraftRequestAmountBasedOnEur;
        DraftNetAmount = arg.DraftNetAmount;
        DraftRequestNetAmountBasedOnUsd = arg.DraftRequestNetAmountBasedOnUsd;
        DraftCurrencyTypeId = arg.DraftCurrencyTypeId != 0 ? new(arg.DraftCurrencyTypeId) : null;
        DraftNetCurrencyTypeId = arg.DraftNetCurrencyTypeId != 0 ? new(arg.DraftNetCurrencyTypeId) : null;
        PayingBankName = arg.PayingBankName;
        BrokerBankName = arg.BrokerBankName;
        InterMediateBankName = arg.InterMediateBankName;
        BeneficiaryBankName = arg.BeneficiaryBankName;
        ExchangeWage = arg.ExchangeWage;
        ResponsibilityWageTypeId = arg.ResponsibilityWageTypeId != 0 ? new(arg.ResponsibilityWageTypeId) : null;
        DarftFinalWage = arg.DarftFinalWage;
        BeneficiaryName = arg.BeneficiaryName;
        RegisterCode = arg.RegisterCode;
        AcceptorName = arg.AcceptorName;
        DraftAcceptDate = arg.DraftAcceptDate;
        DraftAcceptTime = arg.DraftAcceptTime;
        IssueReason = arg.IssueReason;
        IsFinished = arg.IsFinished;
        FinishDate = arg.FinishDate;
        FinishedBy = arg.FinishedBy;
        AccountTypeId = arg.AccountTypeId != 0 ? new(arg.AccountTypeId) : null;
        BrokerId = arg.BrokerId != 0 ? new(arg.BrokerId) : null;
        StaffId = arg.StaffId != 0 ? new(arg.StaffId) : null;
        BrokerTypeId = arg.BrokerTypeId != 0 ? new(arg.BrokerTypeId) : null;
        DraftValorStatusId = arg.DraftValorStatusId != 0 ? new(arg.DraftValorStatusId) : null;
        DraftTypeId = arg.DraftTypeId != 0 ? new(arg.DraftTypeId) : null;
        BeneficiaryAccountNumber = arg.BeneficiaryAccountNumber;
        BeneficiaryAddress = arg.BeneficiaryAddress;
        BeneficiaryPhoneNumber = arg.BeneficiaryPhoneNumber;
        BeneficiaryIban = arg.BeneficiaryIban;
        BeneficiaryPassportNumber = arg.BeneficiaryPassportNumber;
        IntermediaryBank = arg.IntermediaryBank;
        AgentBank = arg.AgentBank;
        DraftOrderNumber = arg.DraftOrderNumber;
        DraftStatusId = arg.DraftStatusId != 0 ? new(arg.DraftStatusId) : null;
        CustomerIban = arg.CustomerIban;
        CustomerPhoneNumber = arg.CustomerPhoneNumber;
        CustomerAddress = arg.CustomerAddress;
        CustomerNationalCode = arg.CustomerNationalCode;
        BlockingAmount = arg.BlockingAmount;
        LoanTypeId = arg.LoanTypeId != 0 ? new(arg.LoanTypeId) : null;
        DraftReviewResultId = arg.DraftReviewResultId != 0 ? new(arg.DraftReviewResultId) : null;
        RejectReason = arg.RejectReason;
        CancellationReferenceNumber = arg.CancellationReferenceNumber;
        CancellationDate = arg.CancellationDate;
        CancellationAmount = arg.CancellationAmount;
        CancellationCurrencyTypeId = arg.CancellationCurrencyTypeId != 0 ? new(arg.CancellationCurrencyTypeId) : null;
        CancellationValorNumber = arg.CancellationValorNumber;
        CancellationResaonId = arg.CancellationResaonId != 0 ? new(arg.CancellationResaonId) : null;
        Description = arg.Description;
        DraftDestinationId = arg.DraftDestinationId != 0 ? new(arg.DraftDestinationId) : null;
        IsFromAgentBank = arg.IsFromAgentBank;
        DraftCurrencyOriginId = arg.DraftCurrencyOriginId != 0 ? new(arg.DraftCurrencyOriginId) : null;
        PaymentTypeId = arg.PaymentTypeId != 0 ? new(arg.PaymentTypeId) : null;
        RequestValorId = arg.RequestValorId != 0 ? new(arg.RequestValorId) : null;
        BrokerSecondLevelAddressBookId = arg.BrokerSecondLevelAddressBookId != 0 ? new(arg.BrokerSecondLevelAddressBookId) : null;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        DetailBic = arg.DetailBic;
        OriginAmount = arg.OriginAmount;
        DraftNetAmountBasedOnEur = arg.DraftNetAmountBasedOnEur; DraftNetAmountBasedOnUsd = arg.DraftNetAmountBasedOnUsd;
        BeneficiaryExternalAccountNumber = arg.BeneficiaryExternalAccountNumber;
        OrderingExternalAccountNumber = arg.OrderingExternalAccountNumber;



    }
    #region Methods

    public static TrustyDraft Create(CreateTrustyDraftArg arg)
    {
        return new TrustyDraft(arg);
    }

    public void CreateFinal(CreateFinalTrustyDraftArg arg)
    {
        if (arg.InquiryRequestId.HasValue) InquiryRequestId = new(arg.InquiryRequestId.Value);
        WageDeductionMethodId = new(arg.WageDeductionMethodId);
        IssueId = new IssueId(arg.IssueId);
        DraftTypeId = new(arg.DraftTypeId);
        DraftIssueWage = arg.DraftIssueWage;
        BranchLetterNumber = arg.BranchLetterNumber;
        CreatedAt = arg.CreatedAt;
        MainShareFromWage = arg.MainShareFromWage;
        BuyShareFromWage = arg.BuyShareFromWage;
        DraftNumber = arg.DraftNumber;
        AddDomainEvent(new CreateTrustyDraftEvent(issueId: arg.IssueId, MainAggregateType: MainAggregateEnums.TrustyDraft,
            Name: arg.DraftNumber, SourceId: arg.Id, IssuePriority: null, DueDate: null, null));

    }
    public void Modify(ModifyTrustyDraftArg arg)
    {
        if (arg.InquiryRequestId.HasValue) InquiryRequestId = new(arg.InquiryRequestId.Value);
        WageDeductionMethodId = new(arg.WageDeductionMethodId);
        AddDomainEvent(new CreateTrustyDraftEvent(issueId: arg.IssueId, MainAggregateType: MainAggregateEnums.TrustyDraft,
            Name: arg.DraftNumber, SourceId: arg.Id, IssuePriority: null, DueDate: null, null));
        //DraftIssueDate = arg.IssueDueDate;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
        DraftTypeId = new(arg.DraftTypeId);
        DraftIssueWage = arg.DraftIssueWage;
    }

    public void UpdateBrokerId(long brokerId)
    {
        BrokerId = new(brokerId);
    }


    public void AddTrustDraftDocument(List<CreateTrustyDraftDocumentArg> args, long TrustyDraftId, long userId)
    {
        var previousDocument = _trustyDraftDocuments.Where(x => x.TrustyDraftId == new TrustyDraftId(TrustyDraftId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addDocument = args.Where(x => !previousDocument.Any(c => c.DocumentId.Value == x.DocumentId)).ToList();
        var deleteDocument = previousDocument.Where(x => !args.Any(c => c.DocumentId == x.DocumentId.Value)).ToList();


        foreach (var document in addDocument)
        {
            var entity = _trustyDraftDocuments.Where(x => (x.DocumentId == new DocumentId(document.DocumentId) && x.TrustyDraftId == new TrustyDraftId(TrustyDraftId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = TrustyDraftDocument.Create(document);
                _trustyDraftDocuments.Add(entity);
            }
        }

        foreach (var document in deleteDocument)
        {
            document.Delete(userId);
        }
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    #endregion
    #region Properties

    public TrustyDraftId Id { get; private set; }
    public DateTime ValorDate { get; private set; }
    public string DraftNumber { get; private set; }
    public string? DraftNumberBasedOnOrder { get; private set; }
    public BranchId BranchId { get; private set; }
    public virtual Branch Branch { get; private set; }
    public WageDeductionMethodId? WageDeductionMethodId { get; private set; }
    public virtual WageDeductionMethod? WageDeductionMethod { get; private set; }
    public InquiryRequestId? InquiryRequestId { get; private set; }
    public virtual InquiryRequest? InquiryRequest { get; private set; }
    public IssueId? IssueId { get; private set; }
    public virtual Issue? Issue { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public virtual Customer Customer { get; private set; }
    public DraftOriginId? DraftOriginId { get; private set; }
    public virtual DraftOrigin? DraftOrigin { get; private set; }
    public string? CustomerAccountNumber { get; private set; }
    public string? BlockNumber { get; private set; }
    public DateTime? DraftIssueDate { get; private set; }
    public string? BlockingNumber { get; private set; }
    public string? BranchLetterNumber { get; private set; }
    public decimal? DraftRequestAmount { get; private set; }
    public decimal? BuyShareFromWage { get; private set; }
    public decimal? MainShareFromWage { get; private set; }
    public decimal? DraftRequestAmountBasedOnUsd { get; private set; }
    public decimal? DraftRequestAmountBasedOnEur { get; private set; }
    public decimal? DraftNetAmount { get; private set; }
    public decimal? DraftRequestNetAmountBasedOnUsd { get; private set; }
    public decimal? DraftNetAmountBasedOnUsd { get; private set; }
    public decimal? DraftNetAmountBasedOnEur { get; private set; }
    public string? OrderingExternalAccountNumber { get; private set; }
    public CurrencyTypeId? DraftCurrencyTypeId { get; private set; }
    public virtual CurrencyType? DraftCurrencyType { get; private set; }
    public CurrencyTypeId? DraftNetCurrencyTypeId { get; private set; }
    public virtual CurrencyType? DraftNetCurrencyType { get; private set; }
    public string? PayingBankName { get; private set; }
    public string? BrokerBankName { get; private set; }
    public string? InterMediateBankName { get; private set; }
    public string? BeneficiaryBankName { get; private set; }
    public string? BeneficiaryExternalAccountNumber { get; private set; }
    public decimal? DraftIssueWage { get; private set; }
    public decimal? ExchangeWage { get; private set; }
    public ResponsibilityWageTypeId? ResponsibilityWageTypeId { get; private set; }
    public virtual ResponsibilityWageType? ResponsibilityWageType { get; private set; }
    public decimal? DarftFinalWage { get; private set; }
    public string? BeneficiaryName { get; private set; }
    public string? RegisterCode { get; private set; }
    public string? AcceptorName { get; private set; }
    public DateTime? DraftAcceptDate { get; private set; }
    public TimeOnly? DraftAcceptTime { get; private set; }
    public string? IssueReason { get; private set; }
    public string? IsFinished { get; private set; }
    public DateTime? FinishDate { get; private set; }
    public long? FinishedBy { get; private set; }
    public AccountTypeId? AccountTypeId { get; private set; }
    public virtual AccountType? AccountType { get; private set; }
    public BrokerId? BrokerId { get; private set; }
    public virtual Broker? Broker { get; private set; }
    public StaffId? StaffId { get; private set; }
    public virtual Staff? Staff { get; private set; }
    public BrokerTypeId? BrokerTypeId { get; private set; }
    public virtual BrokerType? BrokerType { get; private set; }
    public DraftValorStatusId? DraftValorStatusId { get; private set; }
    public virtual DraftValorStatus? DraftValorStatus { get; private set; }
    public DraftTypeId? DraftTypeId { get; private set; }
    public virtual DraftType? DraftType { get; private set; }
    public string? BeneficiaryAccountNumber { get; private set; }
    public string? BeneficiaryAddress { get; private set; }
    public string? BeneficiaryPhoneNumber { get; private set; }
    public string? BeneficiaryIban { get; private set; }
    public string? BeneficiaryPassportNumber { get; private set; }
    public string? IntermediaryBank { get; private set; }
    public string? AgentBank { get; private set; }
    public string? DraftOrderNumber { get; private set; }
    public DraftStatusId? DraftStatusId { get; private set; }
    public virtual DraftStatus? DraftStatus { get; private set; }
    public string? CustomerIban { get; private set; }
    public string? CustomerPhoneNumber { get; private set; }
    public string? CustomerAddress { get; private set; }
    public string? CustomerNationalCode { get; private set; }
    public decimal? BlockingAmount { get; private set; }
    public LoanTypeId? LoanTypeId { get; private set; }
    public virtual LoanType? LoanType { get; private set; }
    public DraftReviewResultId? DraftReviewResultId { get; private set; }
    public virtual DraftReviewResult? DraftReviewResult { get; private set; }
    public string? RejectReason { get; private set; }
    public string? CancellationReferenceNumber { get; private set; }
    public DateTime? CancellationDate { get; private set; }
    public decimal? CancellationAmount { get; private set; }
    public CurrencyTypeId? CancellationCurrencyTypeId { get; private set; }
    public virtual CurrencyType? CancellationCurrencyType { get; private set; }
    public string? CancellationValorNumber { get; private set; }
    public CancellationResaonId? CancellationResaonId { get; private set; }
    public virtual CancellationResaon? CancellationResaon { get; private set; }
    public string? Description { get; private set; }
    public DraftDestinationId? DraftDestinationId { get; private set; }
    public virtual DraftDestination? DraftDestination { get; private set; }
    public string? IsFromAgentBank { get; private set; }
    public DraftCurrencyOriginId? DraftCurrencyOriginId { get; private set; }
    public virtual DraftCurrencyOrigin? DraftCurrencyOrigin { get; private set; }
    public RequestValorId? RequestValorId { get; private set; }
    public virtual RequestValor? RequestValor { get; private set; }
    public BrokerSecondLevelAddressBookId? BrokerSecondLevelAddressBookId { get; private set; }
    public virtual BrokerSecondLevelAddressBook? BrokerSecondLevelAddressBook { get; private set; }
    public string? DetailBic { get; private set; }
    public decimal? OriginAmount { get; private set; }
    public AgentBankWageShareStatusId? AgentBankWageShareStatusId { get; private set; }
    public virtual AgentBankWageShareStatus? AgentBankWageShareStatus { get; private set; }
    public PaymentTypeId? PaymentTypeId { get; private set; }
    public virtual PaymentType? PaymentType { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    #endregion
    #region Related Entities

    private List<BrokerAddressInfo> _brokerAddressInfos = new();
    public ICollection<BrokerAddressInfo> BrokerAddressInfos => _brokerAddressInfos;

    private List<TrustyDraftDocument> _trustyDraftDocuments = new();
    public ICollection<TrustyDraftDocument> TrustyDraftDocuments => _trustyDraftDocuments;

    private List<TrustyDraftResource> _trustyDraftResources = new();
    public ICollection<TrustyDraftResource> TrustyDraftResources => _trustyDraftResources;

    private List<Statement> _statements = new();
    public ICollection<Statement> Statements => _statements;

    private List<Reconsilation> _reconsilations = new();
    public ICollection<Reconsilation> Reconsilations => _reconsilations;

    private List<PaymentReceiptInfo> _paymentReceiptInfos = new();
    public ICollection<PaymentReceiptInfo> PaymentReceiptInfos => _paymentReceiptInfos;

    private List<ReferralLetterDraftList> _referralLetterDraftList = new();
    public ICollection<ReferralLetterDraftList> ReferralLetterDraftList => _referralLetterDraftList;

    private List<ReferralLetter> _referralLetters = new();
    public ICollection<ReferralLetter> ReferralLetters => _referralLetters;
    #endregion
}