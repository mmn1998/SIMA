using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.BrokerTypes
{
    public class BrokerTypeDomainService : IBrokerTypeDomainService
    {
        private readonly SIMADBContext _context;

        public BrokerTypeDomainService(SIMADBContext context)
        {
            _context = context;
        }

        public async Task<bool> IsCodeUnique(string code, long Id)
        {
            if (Id > 0)
                return await _context.BrokerTypes.AnyAsync(x => x.Code == code && x.Id != new BrokerTypeId(Id));
            else
                return await _context.BrokerTypes.AnyAsync(x => x.Code == code);

        }
    }
}
