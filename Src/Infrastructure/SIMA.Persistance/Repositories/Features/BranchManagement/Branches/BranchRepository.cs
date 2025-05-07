using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.Branches;

public class BranchRepository : Repository<Branch>, IBranchRepository
{
    private readonly SIMADBContext _context;

    public BranchRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Branch> GetById(long id)
    {
        var stronglyTypeId = new BranchId(id);
        var entity = await _context.Branches.FirstOrDefaultAsync(b => b.Id == stronglyTypeId);
        entity.NullCheck();
        return entity;
    }

    public async Task<Branch> GetByCode(string code)
    {
        var entity = await _context.Branches.FirstOrDefaultAsync(b => b.Code == code);
        return entity;
    }
}
