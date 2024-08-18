using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.ApiAuthenticationMethods
{
    public class ApiAuthenticationMethodRepository : Repository<ApiAuthenticationMethod>, IApiAuthenticationMethodRepository
    {
        private readonly SIMADBContext _context;

        public ApiAuthenticationMethodRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ApiAuthenticationMethod> GetById(ApiAuthenticationMethodId Id)
        {
            var entity = await _context.ApiAuthenticationMethods.FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }
    }
}
