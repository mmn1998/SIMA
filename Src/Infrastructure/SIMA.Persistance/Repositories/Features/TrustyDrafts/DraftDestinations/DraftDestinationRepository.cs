using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.DraftDestinations
{
    public class DraftDestinationRepository : Repository<DraftDestination>, IDraftDestinationRepository
    {
        private readonly SIMADBContext _context;

        public DraftDestinationRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DraftDestination> GetById(DraftDestinationId Id)
        {
            var entity = await _context.DraftDestinations.FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }

        public async Task<DraftDestination> GetByCode(string code)
        {
            var entity = await _context.DraftDestinations.FirstOrDefaultAsync(x => x.Code == code);
            return entity ;
        }
    }
}
