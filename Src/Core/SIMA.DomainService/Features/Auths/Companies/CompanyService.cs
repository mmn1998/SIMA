using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Companies.Interfaces;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.Companies;

public class CompanyService : ICompanyService
{
    private readonly SIMADBContext _context;

    public CompanyService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.Companies.AnyAsync(b => b.Code == code && b.Id != new CompanyId(id));
        else
            return !await _context.Companies.AnyAsync(b => b.Code == code);
    }

    public async Task<bool> IsCompanyParent(long? companyId)
    {
        bool result = false;
        if (companyId.HasValue)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == new CompanyId(companyId.Value));
            if (company is not null)
            {
                result = company.ParentId?.Value == null;
            }
        }
        return result;
    }
}
