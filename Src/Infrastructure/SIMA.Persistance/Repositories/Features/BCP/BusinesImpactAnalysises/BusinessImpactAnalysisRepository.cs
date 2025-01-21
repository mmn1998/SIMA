using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.BusinesImpactAnalysises;

public class BusinessImpactAnalysisRepository : Repository<BusinessImpactAnalysis>, IBusinessImpactAnalysisRepository
{
    private readonly SIMADBContext _context;

    public BusinessImpactAnalysisRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BusinessImpactAnalysis> GetById(BusinessImpactAnalysisId Id)
    {
        var entity = await _context.BusinessImpactAnalysis
            .Include(x => x.BusinessImpactAnalysisAssets)
            .Include(x => x.BusinessImpactAnalysisStaff)
            .Include(x => x.BusinessImpactAnalysisDocuments)
            .Include(x => x.BusinessImpactAnalysisDisasterOrigins)
            .FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<BusinessImpactAnalysis?> GetLast()
    {
        var entity = await _context.BusinessImpactAnalysis.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        return entity;
    }
}