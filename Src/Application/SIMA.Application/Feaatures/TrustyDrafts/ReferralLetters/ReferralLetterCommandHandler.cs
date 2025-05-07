using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.ReferralLetters;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.Issues;
using SIMA.Resources;
using System.Globalization;

namespace SIMA.Application.Feaatures.TrustyDrafts.ReferralLetters;

public class ReferralLetterCommandHandler : ICommandHandler<CreateReferralLetterCommand, Result<CreateReferralLetterResult>>,
ICommandHandler<ModifyReferralLetterCommand, Result<long>>, ICommandHandler<DeleteReferralLetterCommand, Result<long>>
{
    private readonly IReferalLetterRepository _repository;
    private readonly IReferalLetterDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;
    private readonly ITrustyDraftRepository _trustyDraftRepository;
    private readonly IInquiryRequestRepository _inquiryRequestRepository;
    private readonly IInquiryResponseRepository _inquiryResponseRepository;
    private readonly IIssueQueryRepository _issueQueryRepository;

    public ReferralLetterCommandHandler(IReferalLetterRepository repository, IReferalLetterDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper, ITrustyDraftRepository trustyDraftRepository,
        IInquiryRequestRepository inquiryRequestRepository, IInquiryResponseRepository inquiryResponseRepository,
        IIssueQueryRepository issueQueryRepository)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
        _trustyDraftRepository = trustyDraftRepository;
        _inquiryRequestRepository = inquiryRequestRepository;
        _inquiryResponseRepository = inquiryResponseRepository;
        _issueQueryRepository = issueQueryRepository;
    }
    public async Task<Result<CreateReferralLetterResult>> Handle(CreateReferralLetterCommand request, CancellationToken cancellationToken)
    {
        var trustyEntity = await _trustyDraftRepository.GetById(new TrustyDraftId(request.TrustyDraftId));
        var issueResult = await _issueQueryRepository.ComponentIssue(trustyEntity.Id.Value, trustyEntity.IssueId.Value);
        if (issueResult.IssueInfo.CurrentStepName != "ارجاع به کارگزاری")
            throw new SimaResultException(CodeMessges._400Code, Messages.RefferalLetterNotAllowCreate);


        var arg = _mapper.Map<CreateReferalLetterArg>(request);
        if (arg.LetterDocumentId <= 0) arg.LetterDocumentId = null;
        arg.LetterNumber = await CalculateLetterNumber(arg.BrokerId);
        arg.CreatedBy = _simaIdentity.UserId;
        if (trustyEntity.InquiryRequestId is not null)
        {
            var inqueryRequestEntity = await _inquiryRequestRepository.GetById(trustyEntity.InquiryRequestId);
            var inqueryResponseEntity = await _inquiryResponseRepository.GetByInqueryRequestId(inqueryRequestEntity.Id);

            if (inqueryResponseEntity is not null)
            {
                if (arg.BrokerId != inqueryResponseEntity.BrokerId.Value)
                    throw new SimaResultException(CodeMessges._400Code, Messages.BrokerNotValid);
            }
        }
        else
        {
            trustyEntity.UpdateBrokerId(arg.BrokerId);
        }


        var createResult = new CreateReferralLetterResult
        {
            Id = arg.Id,
            TrustyDraftId = trustyEntity.Id.Value,
            IssueId = trustyEntity.IssueId.Value,
            NextStepId = issueResult.RelatedProgressList.Select(x=>x.TargetId).FirstOrDefault(),
            ProgressId = issueResult.RelatedProgressList.Select(x => x.ProgressId).FirstOrDefault()
        };

        var entity = await ReferralLetter.Create(arg);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(createResult);
    }

    public async Task<Result<long>> Handle(ModifyReferralLetterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyReferalLetterArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteReferralLetterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    private async Task<string> CalculateLetterNumber(long brokerId)
    {
        var value = string.Empty;
        var code = await _service.GetBrokerTypeCodeByBrokerId(new(brokerId));
        var lastLetterNumber = await _service.GetLastLetterNumber();
        var pc = new PersianCalendar();
        var year = pc.GetYear(DateTime.Now).ToString();
        if (string.IsNullOrEmpty(code))
        {
            throw new SimaResultException(CodeMessges._100105Code, Messages.NoBrokerTypeForBrokerIdError);
        }
        var counter = "000001";
        if (!string.IsNullOrEmpty(lastLetterNumber) && lastLetterNumber.Length >= 12)
        {
            var lastCounter = lastLetterNumber.Substring(6, 6);
            counter = (Convert.ToInt64(lastCounter) + 1).ToString();
        }
        value = $@"{year}{code}{counter}";
        return value;
    }
}
