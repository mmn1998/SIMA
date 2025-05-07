using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.ValueObjects;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.WorkFlowEngine.ApprovalOptions
{
    public class ApprovalOptionRepository : Repository<ApprovalOption>, IApprovalOptionRepository
    {
        private readonly SIMADBContext _context;
        public ApprovalOptionRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ApprovalOption> GetById(long id)
        {
            return await _context.ApprovalOptions.FirstOrDefaultAsync(x => x.Id == new ApprovalOptionId(id));
        }
    }
}
