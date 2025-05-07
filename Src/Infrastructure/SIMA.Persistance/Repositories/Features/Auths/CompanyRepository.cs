using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Companies;
using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.Persistance.Repositories.Features.Auths
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly SIMADBContext _context;

        public CompanyRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Company> GetById(long id)
        {
            var entity = await _context.Companies.FirstOrDefaultAsync(c => c.Id == new CompanyId(id));
            if (entity is null) throw new SimaResultException(CodeMessges._100052Code, Messages.CompanyNotFoundError);
            return entity;
        }
    }
}
