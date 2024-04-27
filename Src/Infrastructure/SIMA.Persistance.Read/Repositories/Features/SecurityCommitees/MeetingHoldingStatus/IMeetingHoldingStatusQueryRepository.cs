using SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingStatus;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.MeetingHoldingStatus
{
    public interface IMeetingHoldingStatusQueryRepository : IQueryRepository
    {
        Task<GetMeetingHoldingStatusQueryResult> GetById(long Id);
        Task<Result<List<GetMeetingHoldingStatusQueryResult>>> GetAll(GetAllMeetingHoldingStatusQuery request);
    }
}
