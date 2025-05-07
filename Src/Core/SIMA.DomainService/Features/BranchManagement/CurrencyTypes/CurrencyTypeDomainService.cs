using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.CurrencyTypes
{
    public class CurrencyTypeDomainService : ICurrencyTypeDomainService
    {
        private readonly SIMADBContext _context;

        public CurrencyTypeDomainService(SIMADBContext context)
        {
            _context = context;
        }

        public async Task<bool> IsBaseCurrencyUnique() =>
            await _context.CurrencyTypes.AnyAsync(x => x.IsBaseCurrency == "1");

        public async Task<bool> IsCodeUnique(string code, long Id)
        {
            if (Id > 0)
                return await _context.CurrencyTypes.AnyAsync(x => x.Code == code && x.Id != new CurrencyTypeId(Id));
            else
                return await _context.CurrencyTypes.AnyAsync(x => x.Code == code);
        }
        public async Task<CurrencyType> IsBaseCurrency() =>
            await _context.CurrencyTypes.FirstOrDefaultAsync(x => x.IsBaseCurrency == "1");
    }
}
