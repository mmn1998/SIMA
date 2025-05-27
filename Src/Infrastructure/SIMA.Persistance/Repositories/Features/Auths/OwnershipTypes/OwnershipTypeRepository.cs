using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Entities;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.OwnershipTypes;

public class OwnershipTypeRepository : Repository<OwnershipType>, IOwnershipTypeRepository
{
    private readonly SIMADBContext _context;

    public OwnershipTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<OwnershipType> GetById(OwnershipTypeId Id)
    {
        var entity = await _context.OwnershipTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}