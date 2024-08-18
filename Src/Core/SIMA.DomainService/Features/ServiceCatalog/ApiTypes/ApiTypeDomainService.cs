using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.ApiTypes
{
    public class ApiTypeDomainService : IApiTypeDomainService
    {
        private readonly SIMADBContext _context;

        public ApiTypeDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, ApiTypeId? Id = null)
        {
            bool result = false;
            if (Id == null) result = !await _context.ApiTypes.AnyAsync(x => x.Code == code);
            else result = !await _context.ApiTypes.AnyAsync(x => x.Code == code && x.Id != Id);
            return result;
        }
    }
}
