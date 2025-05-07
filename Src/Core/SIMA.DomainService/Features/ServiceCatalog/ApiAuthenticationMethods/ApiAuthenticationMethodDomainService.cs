using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.ApiAuthenticationMethods
{
    public class ApiAuthenticationMethodDomainService : IApiAuthenticationMethodDomainService
    {
        private readonly SIMADBContext _context;

        public ApiAuthenticationMethodDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, ApiAuthenticationMethodId? Id = null)
        {
            bool result = false;
            if (Id == null) result = !await _context.ApiAuthenticationMethods.AnyAsync(x => x.Code == code);
            else result = !await _context.ApiAuthenticationMethods.AnyAsync(x => x.Code == code && x.Id != Id);
            return result;
        }
    }
}
