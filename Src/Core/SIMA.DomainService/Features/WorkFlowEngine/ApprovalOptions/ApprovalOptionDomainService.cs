using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.WorkFlowEngine.ApprovalOptions
{
    public class ApprovalOptionDomainService : IApprovalOptionDomainService
    {
        private readonly SIMADBContext _context;

        public ApprovalOptionDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return await _context.ApprovalOptions.AnyAsync(b => b.Code == code && b.Id != new ApprovalOptionId(id));
            else
            {
                var result = await _context.ApprovalOptions.AnyAsync(b => b.Code == code);
                return result;
            }
        }
    }
}
