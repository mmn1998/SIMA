using SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingStatus;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.MeetingHoldingStatus;

namespace SIMA.Application.Query.Features.SecurityCommitees.MeetingHoldingStatus
{
    public class MeetingHoldingStatusQueryHandler : IQueryHandler<GetMeetingHoldingStatusQuery, Result<GetMeetingHoldingStatusQueryResult>>,
    IQueryHandler<GetAllMeetingHoldingStatusQuery, Result<IEnumerable<GetMeetingHoldingStatusQueryResult>>>
    {
        private readonly IMeetingHoldingStatusQueryRepository _repository;

        public MeetingHoldingStatusQueryHandler(IMeetingHoldingStatusQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<GetMeetingHoldingStatusQueryResult>>> Handle(GetAllMeetingHoldingStatusQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetMeetingHoldingStatusQueryResult>> Handle(GetMeetingHoldingStatusQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            return Result.Ok(entity);
        }
    }
}
