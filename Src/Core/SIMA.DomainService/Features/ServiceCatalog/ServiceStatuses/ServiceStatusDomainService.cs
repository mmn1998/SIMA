using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Interfaces;
using SIMA.Persistance.Persistence;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceStatuses;

namespace SIMA.DomainService.Features.ServiceCatalog.ServiceStatuses
{
    public class ServiceStatusDomainService : IServiceStatusDomainService
    {
        private readonly SIMADBContext _context;

        public ServiceStatusDomainService(SIMADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCodeUnique(string code, ServiceStatusId? Id = null)
        {
            bool result = false;
            if (Id == null) result = !await _context.ServiceStatuses.AnyAsync(x => x.Code == code);
            else result = !await _context.ServiceStatuses.AnyAsync(x => x.Code == code && x.Id != Id);
            return result;
        }
    }
}
