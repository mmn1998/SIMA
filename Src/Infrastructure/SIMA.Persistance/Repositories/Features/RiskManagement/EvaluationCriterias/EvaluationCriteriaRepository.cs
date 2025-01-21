using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.EvaluationCriterias;

public class EvaluationCriteriaRepository : Repository<EvaluationCriteria>, IEvaluationCriteriaRepository
{
    private readonly SIMADBContext _context;

    public EvaluationCriteriaRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<EvaluationCriteria> GetById(EvaluationCriteriaId Id)
    {
        var entity = await _context.EvaluationCriterias.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}