using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.Categories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly SIMADBContext _context;

    public CategoryRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Category> GetById(CategoryId Id)
    {
        var entity = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}