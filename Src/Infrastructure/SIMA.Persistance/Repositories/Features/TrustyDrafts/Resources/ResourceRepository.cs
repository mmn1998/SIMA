using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.Resources;

public class ResourceRepository : Repository<Resource>, IResourceRepository
{
    private readonly SIMADBContext _context;

    public ResourceRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Resource> GetById(ResourceId Id)
    {
        var entity = await _context.Resources.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}