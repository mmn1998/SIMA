using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.RiskCriterias;

public class RiskCriteriaRepository : Repository<RiskCriteria>, IRiskCriteriaRepository
{
    private readonly SIMADBContext _context;

    public RiskCriteriaRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<RiskCriteria> GetById(long id)
    {
        var result = await _context.RiskCriterias.FirstOrDefaultAsync(it => it.Id == new RiskCriteriaId(id));
        result.NullCheck();
        return result;
    }
}
