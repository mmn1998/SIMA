using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.Interfaces;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class StaffRepository : Repository<Staff>, IStaffRepository
{
    private readonly SIMADBContext _context;

    public StaffRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Staff> GetById(long id)
    {
        var entity = await _context.Staff.FirstOrDefaultAsync(x => x.Id == new StaffId(id));
        entity.NullCheck();
        return entity;
    }
}
