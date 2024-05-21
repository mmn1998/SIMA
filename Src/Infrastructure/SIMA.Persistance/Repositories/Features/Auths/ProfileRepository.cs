using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.Interfaces;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.Persistance.Repositories.Features.Auths
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        private readonly SIMADBContext _context;
        public ProfileRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Profile> GetById(long id)
        {
            var entity = await _context.Profiles.Include(x => x.AddressBooks).FirstOrDefaultAsync(i => i.Id == new ProfileId(id));
            if (entity is null) throw new SimaResultException("10055",Messages.ProfileNotFoundError);
            return entity;
        }
    }
}
