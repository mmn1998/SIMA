using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.BranchType
{
    public class BranchTypeDomainService : IBranchTypeDomainService
    {
        private readonly SIMADBContext _context;

        public BranchTypeDomainService(SIMADBContext context)
        {
            _context = context;
        }

        public async Task<bool> IsCodeUnique(string code, long Id)
        {
            if (Id > 0)
                return await _context.BranchTypes.AnyAsync(x => x.Code == code && x.Id != new BranchTypeId(Id));
            else
                return await _context.BranchTypes.AnyAsync(x => x.Code == code);
        }


    }
}
