using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Departments.Interfaces;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.Departments;

public class DepartmentService : IDepartmentService
{
    private readonly SIMADBContext _context;

    public DepartmentService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.Departments.AnyAsync(b => b.Code == code && b.Id != new DepartmentId(id));
        else
            return !await _context.Departments.AnyAsync(b => b.Code == code);
    }
}
