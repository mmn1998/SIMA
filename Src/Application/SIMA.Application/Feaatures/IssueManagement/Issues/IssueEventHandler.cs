using AutoMapper;
using MediatR;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Events;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Events;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Events;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Security;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueWeightCategories;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlow;
using SIMA.Resources;

namespace SIMA.Application.Feaatures.IssueManagement.Issues
{
    // Mahmoud Domain Event
    // Domain Event Hatman bayad Framework.Core and Framework.Infrustractor update Beshan
    public class IssueEventHandler :
        INotificationHandler<MeetingCreatedEvent>,
        INotificationHandler<RiskCreateEvents>,
        INotificationHandler<CreateLogisticsRequestEvent>,
        INotificationHandler<ModifyLogisticsRequestEvent>,
        INotificationHandler<DeleteLogisticsRequestEvent>
    {
        private readonly IIssueRepository _repository;
        private readonly IMapper _mapper;
        private readonly IIssueDomainService _service;
        private readonly IWorkFlowRepository _workFlowRepository;
        private readonly IWorkFlowQueryRepository _workFlowQueryRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ISimaIdentity _simaIdentity;
        private readonly IIssueWeightCategoryQueryRepository _issueWeightCategoryRepository;
        private readonly IWorkFlowDomainService _workFlowDomainService;

        public IssueEventHandler(IIssueRepository repository,
          IMapper mapper, IIssueDomainService service,
          IWorkFlowRepository workFlowRepository, IProjectRepository projectRepository,
          ISimaIdentity simaIdentity, IIssueWeightCategoryQueryRepository issueWeightCategoryRepository ,
          IWorkFlowQueryRepository workFlowQueryRepository, IWorkFlowDomainService workFlowDomainService)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
            _workFlowRepository = workFlowRepository;
            _projectRepository = projectRepository;
            _simaIdentity = simaIdentity;
            _issueWeightCategoryRepository = issueWeightCategoryRepository;
            _workFlowQueryRepository = workFlowQueryRepository;
            _workFlowDomainService = workFlowDomainService;
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
                //arg.DueDate = DateTime.Now.AddDays(30);
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
                //arg.DueDate = DateTime.Now.AddDays(30);
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
        public async Task Handle(CreateLogisticsRequestEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var workFlowEnity = await _workFlowRepository.GetWorkFlowByAggregateId(notification.MainAggregateType);

                if (!await _workFlowDomainService.CheckCreateIssueWithActor(workFlowEnity.Id.Value)) 
                    throw new SimaResultException(CodeMessges._400Code , Messages.CreateIssueWithChechActorException);

                var workflow = await _workFlowQueryRepository.GetWorkflowInfoByIdAsync(workFlowEnity.Id.Value);
                var issuePriorityId = await _repository.GetHighestPriority();
                var issueTypeId = await _repository.GetIssueTypeRequest();
                var issueweight = await _repository.GetIssueMiddleWeight();
                var arg = _mapper.Map<CreateIssueArg>(notification);
                arg.CreatedBy = _simaIdentity.UserId;
                arg.CurrenStepId = workflow.TargetStepId;
                arg.CurrentStateId = workflow.TargetStateId;
                arg.CurrentWorkflowId = workflow.Id;
                arg.IssueTypeId = issueTypeId;

                if (notification.IssuePriority > 0)
                    arg.IssuePriorityId = notification.IssuePriority;
                else
                    arg.IssuePriorityId = issuePriorityId;

                if (notification.Weight > 0)
                {
                    arg.IssueWeightCategoryd = await _issueWeightCategoryRepository.GetIdByWeight(notification.Weight);
                    arg.Weight = notification.Weight;
                }
                else
                {
                    arg.IssueWeightCategoryd = issueweight.Item1;
                    arg.Weight = issueweight.Item2;
                }

                if (notification.DueDate != null)
                    arg.DueDate = notification.DueDate;
                else
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
        public async Task Handle(ModifyLogisticsRequestEvent notification, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(notification.issueId);
            var arg = _mapper.Map<ModifyIssueArg>(notification);
            arg.Id = entity.Id.Value;
            arg.ModifiedBy = _simaIdentity.UserId;
            arg.IssueWeightCategoryd = await _issueWeightCategoryRepository.GetIdByWeight((int)notification.weight);
            arg.IssueTypeId = entity.IssueTypeId.Value;
            #region history
            var historyArg = _mapper.Map<CreateIssueChangeHistoryArg>(arg);
            await entity.Modify(arg, _service);
            historyArg.CreatedBy = _simaIdentity.UserId;
            historyArg.CompanyId = entity.CompanyId;
            historyArg.MainAggregateId = entity.MainAggregateId.Value;
            historyArg.SourceId = entity.SourceId;
            historyArg.CurrenStepId = entity.CurrenStepId.Value;
            if (entity.CurrentStateId is not null)
                historyArg.CurrentStateId = entity.CurrentStateId.Value;
            historyArg.Code = entity.Code;
            entity.AddIssueChangeHistory(historyArg);
            #endregion

        }
        public async Task Handle(DeleteLogisticsRequestEvent notification, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(notification.issueId);
            long userId = _simaIdentity.UserId;entity.Delete(userId);
        }
    }
}
