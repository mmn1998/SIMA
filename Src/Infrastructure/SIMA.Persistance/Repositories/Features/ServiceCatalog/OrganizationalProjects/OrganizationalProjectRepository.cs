using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.OrganizationalProjects
{
    public class OrganizationalProjectRepository : Repository<OrganizationalProject>, IOrganizationalProjectRepository
    {
        private readonly SIMADBContext _context;

        public OrganizationalProjectRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrganizationalProject> GetById(OrganizationalProjectId Id)
        {
            var entity = await _context.OrganizationalProjects.FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }
    }
}
