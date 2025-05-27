using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.InherentOccurrenceProbabilities;

public class InherentOccurrenceProbabilityDomainService : IInherentOccurrenceProbabilityDomainService
{
    private readonly SIMADBContext _context;

    public InherentOccurrenceProbabilityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, InherentOccurrenceProbabilityId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.InherentOccurrenceProbabilities.AnyAsync(x => x.Code == code);
        else result = !await _context.InherentOccurrenceProbabilities.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsRelationsUnique(ScenarioHistoryId scenarioHistoryId, MatrixAValueId matrixAValueId, InherentOccurrenceProbabilityId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.InherentOccurrenceProbabilities.Where(x=>x.ActiveStatusId == (long)ActiveStatusEnum.Active).AnyAsync(x => x.ScenarioHistoryId == scenarioHistoryId && x.MatrixAValueId == matrixAValueId);
        else result = !await _context.InherentOccurrenceProbabilities.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Active).AnyAsync(x => x.ScenarioHistoryId == scenarioHistoryId && x.MatrixAValueId == matrixAValueId && x.Id != id);
        return result;
    }
}