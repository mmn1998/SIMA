using SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingReasons;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.MeetingHoldingReasons;

public interface IMeetingHoldingReasonQueryRepository
{
    Task<Result<IEnumerable<GetMeetingHoldingReasonQueryResult>>> GetAll(GetAllMeetingHoldingReasonsQuery request);
    Task<GetMeetingHoldingReasonQueryResult> GetById(long Id);
}
