using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.Interfaces;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    private readonly SIMADBContext _context;

    public DepartmentRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Department> GetById(long id)
    {
        var entity = await _context.Departments.FirstOrDefaultAsync(x => x.Id == new DepartmentId(id));
        if (entity is null) throw new SimaResultException(CodeMessges._100053Code, Messages.DepartmentNotFoundError);
        return entity;
    }
}
