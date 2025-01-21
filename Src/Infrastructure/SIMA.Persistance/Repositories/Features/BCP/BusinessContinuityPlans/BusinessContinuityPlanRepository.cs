using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.BusinessContinuityPlans
{
    public class BusinessContinuityPlanRepository : Repository<BusinessContinuityPlan>, IBusinessContinuityPlanRepository
    {
        private readonly SIMADBContext _context;

        public BusinessContinuityPlanRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BusinessContinuityPlan> GetById(BusinessContinuityPlanId Id)
        {
            var entity = await _context.BusinessContinuityPlans
                .Include(x=>x.BusinessContinuityPlanVersionings)
                    .ThenInclude(i=>i.BusinessContinuityPlanStratgies)
                .Include(x=>x.BusinessContinuityPlanVersionings)
                    .ThenInclude(i=>i.BusinessContinuityPlanServices)
                .Include(x => x.BusinessContinuityPlanVersionings)
                 .ThenInclude(i => i.BusinessContinuityPlanRisks)
                .Include(x => x.BusinessContinuityPlanVersionings)
                    .ThenInclude(i => i.BusinessContinuityPlanCriticalActivities)
                .Include(x => x.BusinessContinuityPlanVersionings)
                    .ThenInclude(i => i.BusinessContinuityPlanRelatedStaff)
                .Include(x => x.BusinessContinuityPlanVersionings)
                 .ThenInclude(i => i.BusinessContinuityPlanAssumptions)

                .FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }

        public async Task<BusinessContinuityPlanVersioning> GetBusinessContinuityPlanVersioningById(BusinessContinuityPlanId Id)
        {
            var entity = await _context.BusinessContinuityPlanVersionings.FirstOrDefaultAsync(x => x.BusinessContinuityPlanId == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }
    }
}
