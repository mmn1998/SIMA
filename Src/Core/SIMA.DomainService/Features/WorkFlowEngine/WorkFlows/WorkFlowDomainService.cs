using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.WorkFlowEngine.WorkFlows
{
    public class WorkFlowDomainService : IWorkFlowDomainService
    {
        private readonly SIMADBContext _context;

        public WorkFlowDomainService(SIMADBContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckWorkFlow(long workflowId)
        {
            bool result = false;

            if (workflowId != 0)
                result = await _context.WorkFlows.AnyAsync(x => x.Id == new WorkFlowId(workflowId));

            return result;
        }

        public async Task<bool> SteteIsCodeUnique(string code, long id)
        {
            if (id > 0)
                return await _context.WorkFlows.SelectMany(x => x.States).AnyAsync(b => b.Code == code && b.Id != new StateId(id));
            else
            {
                var result = await _context.WorkFlows.SelectMany(x => x.States).AnyAsync(b => b.Code == code);
                return result;
            }

        }
    }
}
