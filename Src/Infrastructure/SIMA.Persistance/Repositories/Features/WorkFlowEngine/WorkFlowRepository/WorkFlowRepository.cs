using Microsoft.EntityFrameworkCore;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.WorkFlowEngine.WorkFlowRepository;

public class WorkFlowRepository : Repository<WorkFlow>, IWorkFlowRepository
{
    private readonly SIMADBContext _context;
    public WorkFlowRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<WorkFlow> GetById(long id)
    {
        var workFlowId = new WorkFlowId(Value: id);
        WorkFlow workFlow = null;

        workFlow = await _context.WorkFlows.Include(x => x.States).Include(x => x.Steps).FirstOrDefaultAsync(x => x.Id == workFlowId);

        if (workFlow is null)
            workFlow = await _context.WorkFlows.FirstOrDefaultAsync(x => x.Id == workFlowId);

        return workFlow;
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
        var workFlow = await _context.WorkFlows.Include(x => x.States).Include(x => x.Project).Where(x => x.Project.DomainId == domainId).FirstOrDefaultAsync();
        return workFlow;
    }

    public async Task<GetWorkflowInfoByIdResponseQueryResult> GetWorkflowInfoById(long workFlowId)
    {
        var result = new GetWorkflowInfoByIdResponseQueryResult();
        var workFlow = await _context.WorkFlows
       .Include(x => x.States)
       .Include(x => x.Steps)
       .ThenInclude(x => x.SourceProgresses)
       .FirstOrDefaultAsync(x => x.Id == new WorkFlowId(workFlowId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        if (workFlow is not null)
        {
            var startEventActionTypeId = new ActionTypeId(9);
            var step = workFlow.Steps.Where(s => s.ActionTypeId == startEventActionTypeId).FirstOrDefault();
            var progress = step.SourceProgresses.Where(x => x.SourceId == step.Id).FirstOrDefault();

            var nextStep = workFlow.Steps.Where(x => x.Id == progress.TargetId).FirstOrDefault();
            if (nextStep.StateId is not null)
                result.TargetStateId = nextStep.StateId.Value;
            if (step.StateId is not null)
                result.SourceStateId = step.StateId.Value;

            result.TargetStepId = progress.TargetId.Value;
            result.SourceStepId = step.Id.Value;
            result.Id = workFlowId;
            result.ProjectId = workFlow.ProjectId.Value;
            return result;
        }
        else
        {
            throw IssueExceptions.IssueErrorException;
        }
    }

    public async Task<GetWorkflowInfoByIdResponseQueryResult> GetNextStepById(long workFlowId, long nextStep)
    {
        var result = new GetWorkflowInfoByIdResponseQueryResult();
        var workFlow = await _context.WorkFlows
           .Include(x => x.States)
           .Include(x => x.Steps)
           .FirstOrDefaultAsync(x => x.Id == new WorkFlowId(workFlowId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        if (workFlow is not null)
        {
            var step = workFlow.Steps.Where(s => s.Id == new StepId(nextStep)).FirstOrDefault();

            if (step.StateId is not null) result.SourceStateId = step.StateId.Value;

            result.SourceStepId = step.Id.Value;
            result.Id = workFlowId;

            return result;

        }
        else
        {
            throw IssueExceptions.IssueErrorException;
        }
    }
}
