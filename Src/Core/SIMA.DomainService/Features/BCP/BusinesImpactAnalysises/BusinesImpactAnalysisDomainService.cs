using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.BusinesImpactAnalysises;

public class BusinesImpactAnalysisDomainService : IBusinessImpactAnalysisDomainService
{
    private readonly SIMADBContext _context;

    public BusinesImpactAnalysisDomainService(SIMADBContext context)
    {
        _context = context;
    }
}