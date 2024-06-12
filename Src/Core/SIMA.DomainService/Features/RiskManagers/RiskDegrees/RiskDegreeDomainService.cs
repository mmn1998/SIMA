using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Interfaces;
using SIMA.Persistance.Persistence;
using System.Text.RegularExpressions;

namespace SIMA.DomainService.Features.RiskManagers.RiskDegrees
{
    public class RiskDegreeDomainService : IRiskDegreeDomainService
    {
        private readonly SIMADBContext _context;

        public RiskDegreeDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return !await _context.RiskDegrees.AnyAsync(b => b.Code == code && b.Id != new RiskDegreeId(id));
            else
                return !await _context.RiskDegrees.AnyAsync(b => b.Code == code);
        }

        public bool IsHexCodeValid(string hexCode)
        {
            string hexCodePattern = @"[#][0-9A-Fa-f]{6}\b";
            return Regex.IsMatch(hexCode, hexCodePattern);
        }
    }
}
