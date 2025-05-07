using SIMA.Application.Query.Contract.Features.TrustyDrafts.ReferalLetters;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.ReferralLetters;

public interface IReferralLetterQueryRepository : IQueryRepository
{
    Task<GetReferalLetterQueryResult> GetById(GetReferalLetterQuery request);
    Task<GetReferalLetterQueryResult> GetByLetterNumber(string letterNumber);
    Task<Result<IEnumerable<GetAllReferralLettersQueryResult>>> GetAll(GetAllReferalLettersQuery request);
    Task<Result<IEnumerable<GetAllReferralLettersQueryResult>>> GetAllToSecretariat(GetAllReferralLetterToSecretariatQuery request);
    Task<Result<IEnumerable<GetAllReferralLettersQueryResult>>> GetAllToExchange(GetAllReferralLetterToExchangeQuery request);
}
