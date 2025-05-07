using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Contracts
{
    public interface IReferalLetterRepository : IRepository<ReferralLetter>
    {
        Task<ReferralLetter> GetById(ReferralLetterId id);
    }
}
