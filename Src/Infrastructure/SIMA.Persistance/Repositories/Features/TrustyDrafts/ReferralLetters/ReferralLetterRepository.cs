using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.ReferralLetters
{
    public class ReferralLetterRepository : Repository<ReferralLetter>, IReferalLetterRepository
    {
        private readonly SIMADBContext _context;

        public ReferralLetterRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ReferralLetter> GetById(ReferralLetterId Id)
        {
            var entity = await _context.ReferralLetters.FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }
    }
}
