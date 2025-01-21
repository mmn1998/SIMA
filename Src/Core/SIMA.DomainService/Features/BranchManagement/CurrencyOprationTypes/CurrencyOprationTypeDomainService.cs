using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.CurrencyOprationTypes
{
    public class CurrencyOprationTypeDomainService : ICurrencyOprationTypeDomainService
    {
        private readonly SIMADBContext _context;

        public CurrencyOprationTypeDomainService(SIMADBContext context)
        {
            _context = context;
        }

        public async Task<bool> IsCodeUnique(string code, long Id)
        {
            if (Id > 0)
                return await _context.CurrencyOprationTypes.AnyAsync(x => x.Code == code && x.Id != new CurrencyOprationTypeId(Id));
            else
                return await _context.CurrencyOprationTypes.AnyAsync(x => x.Code == code);
        }
    }
}
