using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.Scenarios.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.Scenarios
{
    public class ScenarioDomainService : IScenarioDomainService
    {
        private readonly SIMADBContext _context;

        public ScenarioDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, ScenarioId? Id = null)
        {
            bool result = false;
            if (Id == null) result = !await _context.Scenarios.AnyAsync(x => x.Code == code);
            else result = !await _context.Scenarios.AnyAsync(x => x.Code == code && x.Id != Id);
            return result;
        }
    }
}
