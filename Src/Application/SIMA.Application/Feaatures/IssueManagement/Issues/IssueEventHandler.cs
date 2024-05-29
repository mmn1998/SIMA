using AutoMapper;
using MediatR;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Events;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Events;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Events;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Framework.Common.Security;
using SIMA.Persistance;

namespace SIMA.Application.Feaatures.IssueManagement.Issues
{
    // Mahmoud Domain Event
    // Domain Event Hatman bayad Framework.Core and Framework.Infrustractor update Beshan
    public class IssueEventHandler : INotificationHandler<MeetingCreatedEvent> , INotificationHandler<RiskCreateEvents>
    {
        private readonly IIssueRepository _repository;
        private readonly IMapper _mapper;
        private readonly IIssueDomainService _service;
        private readonly IWorkFlowRepository _workFlowRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ISimaIdentity _simaIdentity;

        public IssueEventHandler(IIssueRepository repository,
          IMapper mapper, IIssueDomainService service,
          IWorkFlowRepository workFlowRepository, IProjectRepository projectRepository, ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
            _workFlowRepository = workFlowRepository;
            _projectRepository = projectRepository;
            _simaIdentity = simaIdentity;
        }

        public async Task Handle(MeetingCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var workFlowEnity = await _workFlowRepository.GetWorkFlowByAggregateId(notification.MainAggregateType);

                //if (!await _workFlowDomainService.CheckCreateIssueWithActor(workFlowEnity.Id.Value)) throw IssueExceptions.CreateIssueWithChechActorException;

                var workflow = await _workFlowRepository.GetWorkflowInfoById(workFlowEnity.Id.Value);
                var issuePriorityId = _repository.GetHighestPriority();
                var issueTypeId = _repository.GetIssueTypeRequest();
                var issueweight = _repository.GetIssueMiddleWeight();
                var arg = _mapper.Map<CreateIssueArg>(notification);
                arg.CreatedBy = _simaIdentity.UserId;
                arg.CurrenStepId = workflow.TargetStepId;
                arg.CurrentStateId = workflow.TargetStateId;
                arg.CurrentWorkflowId = workflow.Id;
                arg.IssuePriorityId = issuePriorityId.Result;// 7678559058;
                arg.IssueTypeId = issueTypeId.Result;// 4131511500;
                arg.IssueWeightCategoryd = issueweight.Result.Item1;
                arg.Weight = issueweight.Result.Item2;
                arg.DueDate = DateTime.Now.AddDays(30);
                #region GenerateCode

                var project = await _projectRepository.GetById(workflow.ProjectId);
                var lastIsuue = await _repository.GetLastIssue();
                string codde = "";
                if (lastIsuue == null)
                {
                    codde = "356";
                }
                else
                {
                    codde = lastIsuue.Code;
                }
                var code = Convert.ToInt32(codde.Substring(codde.IndexOf("-") + 1)) + 1;
                arg.Code = project.Code + "-" + code.ToString();

                #endregion

                var historyArg = _mapper.Map<CreateIssueChangeHistoryArg>(arg);
                historyArg.CreatedBy = _simaIdentity.UserId;
                var entity = await Issue.Create(arg, _service);

                #region IssueHistory
                var history = _mapper.Map<CreateIssueHistoryArg>(arg);
                history.CreatedBy = _simaIdentity.UserId;
                history.SourceStateId = workflow.SourceStateId;
                history.TargetStateId = workflow.TargetStateId;
                history.SourceStepId = workflow.SourceStepId;
                history.TargetStepId = workflow.TargetStepId;
                entity.AddHistory(history);
                #endregion
                entity.AddIssueChangeHistory(historyArg);
                await _repository.Add(entity);


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task Handle(RiskCreateEvents notification, CancellationToken cancellationToken)
        {
            try
            {
                var workFlowEnity = await _workFlowRepository.GetWorkFlowByAggregateId(notification.MainAggregateType);

                //if (!await _workFlowDomainService.CheckCreateIssueWithActor(workFlowEnity.Id.Value)) throw IssueExceptions.CreateIssueWithChechActorException;

                var workflow = await _workFlowRepository.GetWorkflowInfoById(workFlowEnity.Id.Value);
                var issuePriorityId = _repository.GetHighestPriority();
                var issueTypeId = _repository.GetIssueTypeRequest();
                var issueweight = _repository.GetIssueMiddleWeight();
                var arg = _mapper.Map<CreateIssueArg>(notification);
                arg.CreatedBy = _simaIdentity.UserId;
                arg.CurrenStepId = workflow.TargetStepId;
                arg.CurrentStateId = workflow.TargetStateId;
                arg.CurrentWorkflowId = workflow.Id;
                arg.IssuePriorityId = issuePriorityId.Result;// 7678559058;
                arg.IssueTypeId = issueTypeId.Result;// 4131511500;
                arg.IssueWeightCategoryd = issueweight.Result.Item1;
                arg.Weight = issueweight.Result.Item2;
                arg.DueDate = DateTime.Now.AddDays(30);
                #region GenerateCode

                var project = await _projectRepository.GetById(workflow.ProjectId);
                var lastIsuue = await _repository.GetLastIssue();
                string codde = "";
                if (lastIsuue == null)
                {
                    codde = "356";
                }
                else
                {
                    codde = lastIsuue.Code;
                }
                var code = Convert.ToInt32(codde.Substring(codde.IndexOf("-") + 1)) + 1;
                arg.Code = project.Code + "-" + code.ToString();

                #endregion

                var historyArg = _mapper.Map<CreateIssueChangeHistoryArg>(arg);
                historyArg.CreatedBy = _simaIdentity.UserId;
                var entity = await Issue.Create(arg, _service);

                #region IssueHistory
                var history = _mapper.Map<CreateIssueHistoryArg>(arg);
                history.CreatedBy = _simaIdentity.UserId;
                history.SourceStateId = workflow.SourceStateId;
                history.TargetStateId = workflow.TargetStateId;
                history.SourceStepId = workflow.SourceStepId;
                history.TargetStepId = workflow.TargetStepId;
                entity.AddHistory(history);
                #endregion
                entity.AddIssueChangeHistory(historyArg);
                await _repository.Add(entity);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
