using Microsoft.EntityFrameworkCore;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Resources;
using System.ComponentModel.DataAnnotations;

namespace SIMA.Persistance.Repositories.Features.WorkFlowEngine.WorkFlowRepository;

public class WorkFlowRepository : Repository<WorkFlow>, IWorkFlowRepository
{

    private readonly SIMADBContext _context;
    private readonly ISimaIdentity _simaIdentity;
    public WorkFlowRepository(SIMADBContext context, ISimaIdentity simaIdentity) : base(context)
    {
        _context = context;
        _simaIdentity = simaIdentity;
    }

    public async Task<WorkFlow> GetById(long id)
    {
        try
        {
            var workFlowId = new WorkFlowId(Value: id);
            var workFlow = await _context.WorkFlows
                .Include(x => x.Steps)
                .Include(x => x.States)
                .Include(x => x.Progresses)
                .Include(x => x.WorkFlowCompanies)
                .Include(x => x.Issues)
                    .ThenInclude(x => x.Meetings)
                .Include(x => x.WorkFlowActors)
                    .ThenInclude(x => x.WorkFlowActorSteps)
                .FirstOrDefaultAsync(x => x.Id == workFlowId);

            return workFlow ?? throw new SimaResultException("10057",Messages.WorkflowNotFoundError);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<WorkFlow> GetById2(WorkFlowId id)
    {
        WorkFlow workFlow = await _context.WorkFlows.FirstOrDefaultAsync(x => x.Id == id);

        return workFlow;
    }

    public async Task<Step> GetStepById(long id)
    {
        var step = await _context.WorkFlows.SelectMany(x => x.Steps).Where(x => x.Id == new StepId(id)).FirstOrDefaultAsync();
        return step;
    }

    public async Task<WorkFlow> GetWorkFlowByDomainId(long domainId)
    {
        var workFlow = await _context.WorkFlows.Include(x => x.States).Include(x => x.Project).Where(x => x.Project.DomainId == new DomainId(domainId)).FirstOrDefaultAsync();
        return workFlow;
    }

    public async Task<GetWorkflowInfoByIdResponseQueryResult> GetWorkflowInfoById(long workFlowId)
    {
        var result = new GetWorkflowInfoByIdResponseQueryResult();
        var workFlow = await _context.WorkFlows
       .Include(x => x.States)
       .Include(x => x.Steps)
       .ThenInclude(x => x.SourceProgresses)
       .Include(x => x.WorkFlowActors).ThenInclude(x => x.WorkFlowActorSteps)
       .Include(x => x.WorkFlowActors).ThenInclude(x => x.WorkFlowActorUsers)
       .FirstOrDefaultAsync(x => x.Id == new WorkFlowId(workFlowId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        var startEventActionTypeId = new ActionTypeId((long)ActionTypeEnum.startEvent);

        var steps = workFlow.Steps.Where(x => x.ActionTypeId == startEventActionTypeId).ToList();
        if (steps.Count > 1)
        {
            foreach (var actor in workFlow.WorkFlowActors)
            {
                var sss = actor.WorkFlowActorUsers.Where(x => x.UserId == new UserId(_simaIdentity.UserId) && x.ActiveStatusId != (long)ActiveStatusEnum.Delete).FirstOrDefault();
                if(sss is not null)
                {
                    var actorStep = actor.WorkFlowActorSteps.Select(x => x.StepId);
                    var step = workFlow.Steps.Where(s => actorStep.Contains(s.Id) && s.ActionTypeId == startEventActionTypeId).FirstOrDefault();
                    var progress = step.SourceProgresses.Where(x => x.SourceId == step.Id).FirstOrDefault();
                    var nextStep = workFlow.Steps.Where(x => x.Id == progress.TargetId).FirstOrDefault();

                    if (progress.StateId is not null)
                        result.TargetStateId = progress.StateId.Value;

                    //if (step.StateId is not null)
                    result.SourceStateId = null;

                    result.TargetStepId = progress.TargetId.Value;
                    result.SourceStepId = step.Id.Value;
                    result.Id = workFlowId;
                    result.ProjectId = workFlow.ProjectId.Value;
                    result.MainAggregateId = workFlow.MainAggregateId.Value;
                    return result;

                }
            }
        }
        else
        {
            if (workFlow is not null)
            {
                var step = workFlow.Steps.Where(s => s.ActionTypeId == startEventActionTypeId).FirstOrDefault();
                var progress = step.SourceProgresses.Where(x => x.SourceId == step.Id).FirstOrDefault();
                var nextStep = workFlow.Steps.Where(x => x.Id == progress.TargetId).FirstOrDefault();

                if (progress.StateId is not null)
                    result.TargetStateId = progress.StateId.Value;

                //if (step.StateId is not null)
                result.SourceStateId = null;

                result.TargetStepId = progress.TargetId.Value;
                result.SourceStepId = step.Id.Value;
                result.Id = workFlowId;
                result.ProjectId = workFlow.ProjectId.Value;
                result.MainAggregateId = workFlow.MainAggregateId.Value;
                return result;
            }
            else
            {
                throw new SimaResultException(CodeMessges._400Code, Messages.IssueErrorException);
            }
        }
            return result;
    }

    //public async Task<GetWorkflowInfoByIdResponseQueryResult> GetNextStepById(long workFlowId, long nextStep, long progressId)
    //{
    //    var result = new GetWorkflowInfoByIdResponseQueryResult();
    //    var workFlow = await _context.WorkFlows
    //       .Include(x => x.States)
    //       .Include(x => x.Steps)
    //       .Include(x => x.Progresses)
    //       .FirstOrDefaultAsync(x => x.Id == new WorkFlowId(workFlowId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

    //    if (workFlow is not null)
    //    {
    //        var step = workFlow.Steps.Where(s => s.Id == new StepId(nextStep)).FirstOrDefault();

    //        var progress = workFlow.Progresses.Where(x => x.Id == new ProgressId(progressId)).FirstOrDefault();

    //        if (progress.StateId is not null) result.SourceStateId = progress.StateId.Value;

    //        result.SourceStepId = step.Id.Value;
    //        result.Id = workFlowId;

    //        return result;

    //    }
    //    else
    //    {
    //        throw IssueExceptions.IssueErrorException;
    //    }
    //}

    public Task<WorkFlow> GetWorkFlowByAggregateId(MainAggregateEnums mainAggregate)
    {
        var workFlow = _context.WorkFlows.FirstOrDefaultAsync(x => x.MainAggregateId == new MainAggregateId((long)mainAggregate));
        return workFlow;
    }
}
