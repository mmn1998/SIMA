using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.FinancialActionTypes
{
    public class FinancialActionTypeDomainService : IFinancialActionTypeDomainService
    {
        private readonly SIMADBContext _context;

        public FinancialActionTypeDomainService(SIMADBContext context)
        {
            _context = context;
        }

        public async Task<bool> IsCodeUnique(string code, long Id)
        {
            if (Id > 0)
                return await _context.FinancialActionTypes.AnyAsync(x => x.Code == code && x.Id != new FinancialActionTypeId(Id));
            else
                return await _context.FinancialActionTypes.AnyAsync(x => x.Code == code);
        }
    }
}
