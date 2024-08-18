using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.ServiceCustomerTypes.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.CustomerTypes;

public class CustomerTypeDomainService : ICustomerTypeDomainService
{
    private readonly SIMADBContext _context;

    public CustomerTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, CustomerTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ServiceCustomerTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.ServiceCustomerTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}