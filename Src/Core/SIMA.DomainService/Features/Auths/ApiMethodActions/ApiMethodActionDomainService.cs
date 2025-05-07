using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Contracts;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.ApiMethodActions;

public class ApiMethodActionDomainService : IApiMethodActionDomainService
{
    private readonly SIMADBContext _context;

    public ApiMethodActionDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ApiMethodActionId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ApiMethodActions.AnyAsync(x => x.Code == code);
        else result = !await _context.ApiMethodActions.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}