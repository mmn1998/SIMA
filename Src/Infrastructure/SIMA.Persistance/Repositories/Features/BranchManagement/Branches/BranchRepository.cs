using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.Branches;

public class BranchRepository : Repository<Domain.Models.Features.BranchManagement.Branches.Entities.Branch>, IBranchRepository
{
    private readonly SIMADBContext _context;

    public BranchRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Domain.Models.Features.BranchManagement.Branches.Entities.Branch> GetById(long id)
    {
        var stronglyTypeId = new BranchId(id);
        var entity = await _context.Branches.FirstOrDefaultAsync(b => b.Id == stronglyTypeId);
        entity.NullCheck();
        return entity;
    }
}
