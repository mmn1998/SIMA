using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.BranchTypes;

public class BranchTypeRepository : Repository<BranchType>, IBranchTypeRepository
{
    private readonly SIMADBContext _context;
    public BranchTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<BranchType> GetById(long id)
    {
        var stronglyTypeId = new BranchTypeId(id);
        var entity = await _context.BranchTypes.FirstOrDefaultAsync(pt => pt.Id == stronglyTypeId);
        entity.NullCheck();
        return entity;
    }
}
