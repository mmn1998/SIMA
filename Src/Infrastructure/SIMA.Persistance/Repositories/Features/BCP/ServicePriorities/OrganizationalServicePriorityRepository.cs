using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Entities;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.ServicePriorities;

public class OrganizationalServicePriorityRepository : Repository<OrganizationalServicePriority>, IOrganizationalServicePriorityRepository
{
    private readonly SIMADBContext _context;

    public OrganizationalServicePriorityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<OrganizationalServicePriority> GetById(OrganizationalServicePriorityId Id)
    {
        var entity = await _context.OrganizationalServicePriorities.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}