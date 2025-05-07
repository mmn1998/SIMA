using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.UserTypes.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.UserTypes;

public class UserTypeDomainService : IUserTypeDomainService
{
    private readonly SIMADBContext _context;

    public UserTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, UserTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.UserTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.UserTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}