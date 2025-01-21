using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.AccountTypes;

public class AccountTypeDomainService : IAccountTypeDomainService
{
    private readonly SIMADBContext _context;

    public AccountTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, AccountTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.AccountTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.AccountTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}