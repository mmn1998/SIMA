using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.BusinessContinuityStrategies;

public class BusinessContinuityStrategyRepository : Repository<BusinessContinuityStrategy>, IBusinessContinuityStategyRepository
{
    private readonly SIMADBContext _context;

    public BusinessContinuityStrategyRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BusinessContinuityStrategy> GetById(BusinessContinuityStrategyId Id)
    {
        var entity = await _context.BusinessContinuityStrategies
            .Include(x => x.BusinessContinuityStrategyObjectives)
            .Include(x => x.BusinessContinuityStratgySolutions)
            .Include(x => x.BusinessContinuityStrategyDocuments)
            .Include(x => x.BusinessContinuityStratgyResponsibles)
            .FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<BusinessContinuityStrategy?> GetLast()
    {
        var entity = await _context.BusinessContinuityStrategies.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        return entity;
    }
}