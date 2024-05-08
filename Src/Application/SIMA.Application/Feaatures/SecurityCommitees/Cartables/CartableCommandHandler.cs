using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Contract.Features.SecurityCommitees.Cartables;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Application.Feaatures.SecurityCommitees.Cartables;

public class CartableCommandHandler
{
    private readonly IIssueRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWorkFlowRepository _workFlowRepository;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;

    public CartableCommandHandler(IIssueRepository repository, IUnitOfWork unitOfWork,
        IWorkFlowRepository workFlowRepository, IMapper mapper, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _workFlowRepository = workFlowRepository;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CartableCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.SpName))
        {
            var res = _repository.ExcecuteStoreProcedure(request.SpName);

        }
        if (request.NextStepId != null && request.NextStepId != 0 && request.IssueId.HasValue && request.ProgressId.HasValue && request.ProgressId.HasValue)
        {
            var issue = await _repository.GetById(request.IssueId.Value);
            var nextStep = await _workFlowRepository.GetNextStepById(issue.CurrentWorkflowId.Value, request.NextStepId.Value, request.ProgressId.Value);

            var arg = _mapper.Map<IssueRunActionArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            arg.CurrentStepId = nextStep.SourceStepId;
            arg.CurrentStateId = nextStep.SourceStateId;


            #region IssueHistory

            var history = _mapper.Map<CreateIssueHistoryArg>(issue);
            history.CreatedBy = _simaIdentity.UserId;
            history.SourceStateId = issue.CurrentStateId.Value;
            history.TargetStateId = nextStep.SourceStateId;
            history.SourceStepId = issue.CurrenStepId.Value;
            history.TargetStepId = nextStep.SourceStepId;
            issue.AddHistory(history);

            #endregion

            issue.RunAction(arg);
            await _unitOfWork.SaveChangesAsync();
        }
        return Result.Ok(request.IssueId.Value);
    }
}
