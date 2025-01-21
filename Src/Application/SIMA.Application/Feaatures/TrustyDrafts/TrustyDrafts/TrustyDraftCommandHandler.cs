#region usings
using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Application.Services.BehsazanServices;
using SIMA.Application.Services.BehsazanServices.Request;
using SIMA.Application.Services.BehsazanServices.Response;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Resources;
using System.Globalization;
#endregion

namespace SIMA.Application.Feaatures.TrustyDrafts.TrustyDrafts;

public class TrustyDraftCommandHandler : ICommandHandler<CreateTrustyDraftCommand, Result<long>>,
    ICommandHandler<ModifyTrustyDraftCommand, Result<long>>,
    ICommandHandler<DeleteTrustyDraftCommand, Result<long>>,
    ICommandHandler<TrustCurrencyDraft, Result<GetTrustyDraftInqueryQueryResult>>
{
    #region readonlyFields
    private readonly ITrustyDraftRepository _repository;
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IBehsazanService _behsazanService;
    private readonly ITrustyDraftQueryRepository _trustyDraftQueryRepository;
    private readonly ICurrencyTypeRepository _currencyTypeRepository;
    private readonly IDraftStatusRepository _draftStatusRepository;
    private readonly IDraftStatusDomainService _draftStatusDomainService;
    private readonly ICurrencyTypeDomainService _currencyTypeDomainService;
    private readonly ICustomerRepository _customerRepository;
    private readonly IDraftReviewResultRepository _draftReviewResultRepository;
    private readonly ICancellationResaonRepository _cancellationResaonRepository;
    private readonly ILoanTypeRepository _loanTypeRepository;
    private readonly IRequestValorRepository _requestValorRepository;
    private readonly IDraftTypeRepository _draftTypeRepository;
    private readonly IDraftDestinationRepository _draftDestinationRepository;
    private readonly IResponsibilityWageTypeRepository _responsibilityWageTypeRepository;
    private readonly IDraftValorStatusRepository _draftValorStatusRepository;
    private readonly IAccountTypeRepository _accountTypeRepository;
    private readonly IDraftOriginRepository _draftOriginRepository;
    private readonly IPaymentTypeRepository _paymentTypeRepository;
    private readonly IBrokerTypeRepository _brokerTypeRepository;
    private readonly IInquiryRequestRepository _inquiryRequestRepository;
    #endregion
    #region constructor
    public TrustyDraftCommandHandler(ITrustyDraftRepository repository, IUnitOfWork unitOfWork
        , IMapper mapper, ISimaIdentity simaIdentity,
          IBehsazanService behsazanService, ITrustyDraftQueryRepository trustyDraftQueryRepository,
          IBranchRepository branchRepository, ICurrencyTypeRepository currencyTypeRepository,
          ICurrencyTypeDomainService currencyTypeDomainService, IDraftStatusRepository draftStatusRepository,
          IDraftStatusDomainService draftStatusDomainService, ICustomerRepository customerRepository,
          IDraftReviewResultRepository draftReviewResultRepository, ICancellationResaonRepository cancellationResaonRepository,
          ILoanTypeRepository loanTypeRepository, IRequestValorRepository requestValorRepository,
          IDraftTypeRepository draftTypeRepository, IDraftDestinationRepository draftDestinationRepository,
          IResponsibilityWageTypeRepository responsibilityWageTypeRepository, IDraftValorStatusRepository draftValorStatusRepository,
          IAccountTypeRepository accountTypeRepository, IDraftOriginRepository draftOriginRepository,
          IPaymentTypeRepository paymentTypeRepository, IBrokerTypeRepository brokerTypeRepository,
          IInquiryRequestRepository inquiryRequestRepository
        )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
        _behsazanService = behsazanService;
        _trustyDraftQueryRepository = trustyDraftQueryRepository;
        _branchRepository = branchRepository;
        _currencyTypeRepository = currencyTypeRepository;
        _currencyTypeDomainService = currencyTypeDomainService;
        _draftStatusRepository = draftStatusRepository;
        _draftStatusDomainService = draftStatusDomainService;
        _customerRepository = customerRepository;
        _draftReviewResultRepository = draftReviewResultRepository;
        _cancellationResaonRepository = cancellationResaonRepository;
        _loanTypeRepository = loanTypeRepository;
        _requestValorRepository = requestValorRepository;
        _draftTypeRepository = draftTypeRepository;
        _draftDestinationRepository = draftDestinationRepository;
        _responsibilityWageTypeRepository = responsibilityWageTypeRepository;
        _draftValorStatusRepository = draftValorStatusRepository;
        _accountTypeRepository = accountTypeRepository;
        _draftOriginRepository = draftOriginRepository;
        _paymentTypeRepository = paymentTypeRepository;
        _brokerTypeRepository = brokerTypeRepository;
        _inquiryRequestRepository = inquiryRequestRepository;
    }
    #endregion

    public async Task<Result<GetTrustyDraftInqueryQueryResult>> Handle(TrustCurrencyDraft request, CancellationToken cancellationToken)
    {
        try
        {

            var result = new GetTrustCurrencyDraftMansha();
            var response = new Result<GetTrustyDraftInqueryQueryResult>();
            var darft = await _repository.GetByDarftNumber(request.draftId);

            if (darft is not null)
            {
                if (darft.IssueId is not null)
                    throw new SimaResultException(CodeMessges._400Code, Messages.DraftInProgress);
                else
                    response = await _trustyDraftQueryRepository.GetByIdForInquery(darft.Id.Value);
            }
            else
            {
                try
                {
                    result = await _behsazanService.GetTrustyDraft(request);
                    if (result.Status != 200)
                        throw new SimaResultException(CodeMessges._100100Code, result.Message);
                    else
                    {

                        var arg = _mapper.Map<CreateTrustyDraftArg>(result);
                        arg.CreatedBy = _simaIdentity.UserId;

                        var customer = await _customerRepository.GetByCode(result.Data[0].extCustCd.ToString());
                        if (customer is not null)
                            arg.CustomerId = customer.Id.Value;
                        else
                            throw new SimaResultException(CodeMessges._100100Code, Messages.CustomerIsNull);


                        var draftReview = await _draftReviewResultRepository.GetByName(result.Data[0].rvwRmk?.ToString());
                        if (draftReview is not null) arg.DraftReviewResultId = draftReview.Id.Value;

                        var cancelitionReason = await _cancellationResaonRepository.GetByCode(result.Data[0].revokeReason.ToString());
                        if (cancelitionReason is not null) arg.CancellationResaonId = cancelitionReason.Id.Value;

                        var branch = await _branchRepository.GetByCode(result.Data[0].regBranchCd?.ToString());
                        if (branch is not null) arg.BranchId = branch.Id.Value;

                        var currency = await _currencyTypeRepository.GetByCode(result.Data[0].instructedArzCd.ToString());
                        if (currency is not null) arg.DraftCurrencyTypeId = currency.Id.Value;

                        var draftStatus = await _draftStatusRepository.GetByCode(result.Data[0].draftSts?.ToString());
                        if (draftStatus is not null) arg.DraftStatusId = draftStatus.Id.Value;

                        var loanType = await _loanTypeRepository.GetByCode(result.Data[0].facilityType.ToString());
                        if (loanType is not null) arg.LoanTypeId = loanType.Id.Value;

                        var requestValor = await _requestValorRepository.GetByCode(result.Data[0].reqValueTyp.ToString());
                        if (requestValor is not null) arg.RequestValorId = requestValor.Id.Value;

                        //طبق گفته تیم تحلیل تا اطلاع بهسازاران نوع حواله های بهسازان صادره درج می شود
                        var draftType = await _draftTypeRepository.GetByCode("13");
                        if (draftType is not null) arg.DraftTypeId = draftType.Id.Value;

                        var paymentType = await _paymentTypeRepository.GetByCode(result.Data[0].payTyp.ToString());
                        if (paymentType is not null) arg.PaymentTypeId = paymentType.Id.Value;

                        var draftDestination = await _draftDestinationRepository.GetByCode(result.Data[0].draftDestTyp.ToString());
                        if (draftDestination is not null) arg.DraftDestinationId = draftDestination.Id.Value;

                        var responsibilityWageType = await _responsibilityWageTypeRepository.GetByCode(result.Data[0].chrgTyp.ToString());
                        if (responsibilityWageType is not null) arg.ResponsibilityWageTypeId = responsibilityWageType.Id.Value;

                        var draftValorStatus = await _draftValorStatusRepository.GetByCode(result.Data[0].rvwrslt.ToString());
                        if (draftValorStatus is not null) arg.DraftValorStatusId = draftValorStatus.Id.Value;

                        var accountType = await _accountTypeRepository.GetByCode(result.Data[0].crspndntAccTyp.ToString());
                        if (accountType is not null) arg.AccountTypeId = accountType.Id.Value;

                        var draftOrign = await _draftOriginRepository.GetByCode(result.Data[0].draftFrom.ToString());
                        if (draftOrign is not null) arg.DraftOriginId = draftOrign.Id.Value;

                        var brokerType = await _brokerTypeRepository.GetByCode(result.Data[0].issuanceSource.ToString());
                        if (result.Data[0].issuanceSource is not null && (result.Data[0].issuanceSource == 0 || result.Data[0].issuanceSource == 2 || result.Data[0].issuanceSource == 3))
                            brokerType = await _brokerTypeRepository.GetByCode("6"); // راهبر
                        else
                            brokerType = await _brokerTypeRepository.GetByCode("1"); // صرافی

                        if (brokerType is not null) arg.BrokerTypeId = brokerType.Id.Value;





                        var entity = TrustyDraft.Create(arg);

                        await _repository.Add(entity);
                        await _unitOfWork.SaveChangesAsync();
                        response = await _trustyDraftQueryRepository.GetByIdForInquery(entity.Id.Value);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                    //if (!string.IsNullOrEmpty(result.Message))
                    //    throw new SimaResultException(CodeMessges._100100Code, result.Message);
                    //else
                    //    throw new SimaResultException(CodeMessges._100100Code, Messages.ErrorFromBehsazanService);

                }
            }
            return response;
        }
        catch (Exception ex)
        {

            throw;
        }



    }
    public async Task<Result<long>> Handle(CreateTrustyDraftCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetById(new(request.Id));
            var userBranchId = await _trustyDraftQueryRepository.GetBranchIdByUserId(_simaIdentity.UserId);
            if (entity.BranchId.Value != userBranchId) throw new SimaResultException(CodeMessges._100107Code, Messages.UserIsNotInSameBranch);
            var inqueryRequest = await _inquiryRequestRepository.GetById(new InquiryRequestId(request.InquiryRequestId.Value));
            var brokerTypeIdInInquiryRequest = await _trustyDraftQueryRepository.GetBrokerTypeIdFromInquiryRequestId(request.InquiryRequestId.Value);
            if (brokerTypeIdInInquiryRequest != entity.BrokerTypeId?.Value) throw new SimaResultException(CodeMessges._100106Code, Messages.BrokerTypeErrorInTrustyDraftFinal);

            var inquiryResponse = inqueryRequest.InquiryResponses.FirstOrDefault() ?? throw SimaResultException.NotFound;
            var validCurrency = inqueryRequest.InquiryRequestCurrencies.FirstOrDefault(x => x.Id == inquiryResponse.InquiryRequestCurrencyId) ?? throw SimaResultException.NotFound;

            if (entity.DraftRequestAmount != validCurrency.Amount)
                throw new SimaResultException(CodeMessges._400Code, Messages.DraftRequetAmountError);

            if (entity.DraftCurrencyTypeId != validCurrency.CurrencyTypeId)
                throw new SimaResultException(CodeMessges._400Code, Messages.CurrencyTypeError);

            var arg = _mapper.Map<CreateFinalTrustyDraftArg>(request);
            arg.DraftNumber = entity.DraftNumber;
            arg.BranchLetterNumber = await GetBranchLetterNumber();
            if (request.TrustDraftDocumentList is not null && request.TrustDraftDocumentList.Count > 0)
            {
                var documentArg = _mapper.Map<List<CreateTrustyDraftDocumentArg>>(request.TrustDraftDocumentList);
                foreach (var item in documentArg)
                {
                    item.TrustyDraftId = entity.Id.Value;
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddTrustDraftDocument(documentArg, entity.Id.Value, _simaIdentity.UserId);

            }

            entity.CreateFinal(arg);

            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    public async Task<Result<long>> Handle(ModifyTrustyDraftCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyTrustyDraftArg>(request);
        arg.DraftNumber = entity.DraftNumber;
        entity.Modify(arg);

        if (request.TrustDraftDocumentList is not null)
        {
            var documentArg = _mapper.Map<List<CreateTrustyDraftDocumentArg>>(request.TrustDraftDocumentList);
            foreach (var item in documentArg)
            {
                item.TrustyDraftId = entity.Id.Value;
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.AddTrustDraftDocument(documentArg, entity.Id.Value, _simaIdentity.UserId);
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(DeleteTrustyDraftCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    private async Task<string> GetBranchLetterNumber()
    {
        var value = string.Empty;
        var pc = new PersianCalendar();
        var year = pc.GetYear(DateTime.Now).ToString();
        value = $@"{year}000001";
        var lastBranchLetterNumber = await _trustyDraftQueryRepository.GetLastBranchLetterNumber();
        if (!string.IsNullOrEmpty(lastBranchLetterNumber))
        {
            if (lastBranchLetterNumber.Contains(year))
            {
                var convertedNumber = Convert.ToInt64(lastBranchLetterNumber);
                convertedNumber = convertedNumber + 1;
                value = convertedNumber.ToString("000000");
            }
        }
        return value;
    }
}
