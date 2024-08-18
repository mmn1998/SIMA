using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.ServiceStatuses
{
    public class ServiceStatusRepository : Repository<ServiceStatus>, IServiceStatusRepository
    {
        private readonly SIMADBContext _context;

        public ServiceStatusRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ServiceStatus> GetById(ServiceStatusId Id)
        {
            var entity = await _context.ServiceStatuses.FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }
    }
}
