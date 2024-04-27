using AutoMapper;
using MediatR;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Events;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Events;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Persistance;

namespace SIMA.Application.Feaatures.IssueManagement.Issues
{
    // Mahmoud Domain Event
    // Domain Event Hatman bayad Framework.Core and Framework.Infrustractor update Beshan
    public class IssueEventHandler : INotificationHandler<MeetingCreatedEvent>
    {
        private readonly IIssueRepository _repository;
        private readonly IMapper _mapper;
        private readonly IIssueDomainService _service;
        private readonly IWorkFlowRepository _workFlowRepository;
        private readonly IProjectRepository _projectRepository;

        public IssueEventHandler(IIssueRepository repository,
          IMapper mapper, IIssueDomainService service,
          IWorkFlowRepository workFlowRepository, IProjectRepository projectRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
            _workFlowRepository = workFlowRepository;
            _projectRepository = projectRepository;
        }

        public async Task Handle(MeetingCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var workFlowEnity = await _workFlowRepository.GetWorkFlowByAggregateId(notification.MainAggregateType);

                //if (!await _workFlowDomainService.CheckCreateIssueWithActor(workFlowEnity.Id.Value)) throw IssueExceptions.CreateIssueWithChechActorException;

                var workflow = await _workFlowRepository.GetWorkflowInfoById(workFlowEnity.Id.Value);

                var arg = _mapper.Map<CreateIssueArg>(notification);
                arg.CurrenStepId = workflow.TargetStepId;
                arg.CurrentStateId = workflow.TargetStateId;
                arg.CurrentWorkflowId = workflow.Id;
                arg.IssuePriorityId = 7678559058;
                arg.IssueTypeId = 4131511500;
                arg.IssueWeightCategoryd = 432435;
                arg.Weight = 10;
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
                var entity = await Issue.Create(arg, _service);

                #region IssueHistory
                var history = _mapper.Map<CreateIssueHistoryArg>(arg);
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
