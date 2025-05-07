using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriorities.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriorities.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.ServicePriorities
{
    public class ServicePriorityRepository : Repository<ServicePriority>, IServicePriorityRepository
    {
        private readonly SIMADBContext _context;

        public ServicePriorityRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ServicePriority> GetById(ServicePriorityId Id)
        {
            var entity = await _context.ServicePriorities
           .FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }
    }
}
