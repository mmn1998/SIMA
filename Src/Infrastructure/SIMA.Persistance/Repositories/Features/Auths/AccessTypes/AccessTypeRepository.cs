using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AccessTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.AccessTypes;

public class AccessTypeRepository : Repository<AccessType>, IAccessTypeRepository
{
    private readonly SIMADBContext _context;

    public AccessTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<AccessType> GetById(AccessTypeId Id)
    {
        var entity = await _context.AccessTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}