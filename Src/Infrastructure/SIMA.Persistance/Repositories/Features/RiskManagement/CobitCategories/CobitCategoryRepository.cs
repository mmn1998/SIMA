using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.CobitCategories;

public class CobitCategoryRepository : Repository<CobitCategory>, ICobitCategoryRepository
{
    private readonly SIMADBContext _context;

    public CobitCategoryRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<CobitCategory> GetById(CobitCategoryId id)
    {
        return await _context.CobitCategories.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}