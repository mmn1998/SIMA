using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.FinancialActionTypes
{
    public class FinancialActionTypeRepository : Repository<FinancialActionType>, IFinancialActionTypeRepository
    {
        private readonly SIMADBContext _context;

        public FinancialActionTypeRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FinancialActionType> GetById(long id)
        {
            var stronglyTypeId = new FinancialActionTypeId(id);
            var entity = await _context.FinancialActionTypes.FirstOrDefaultAsync(pt => pt.Id == stronglyTypeId);
            entity.NullCheck();
            return entity;
        }
    }
}
