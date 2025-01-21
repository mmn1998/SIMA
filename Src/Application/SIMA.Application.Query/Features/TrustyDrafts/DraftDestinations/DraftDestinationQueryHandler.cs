using SIMA.Application.Contract.Features.TrustyDrafts.DraftDestinations;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftDestinations;

namespace SIMA.Application.Query.Features.TrustyDrafts.DraftDestinations
{
    public class DraftDestinationQueryHandler : IQueryHandler<GetDraftDestinationQuery, Result<GetDraftDestinationQueryResult>>,
    IQueryHandler<GetAllDraftDestinationsQuery, Result<IEnumerable<GetDraftDestinationQueryResult>>>
    {
        private readonly IDraftDestinationQueryRepository _repository;

        public DraftDestinationQueryHandler(IDraftDestinationQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetDraftDestinationQueryResult>> Handle(GetDraftDestinationQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetDraftDestinationQueryResult>>> Handle(GetAllDraftDestinationsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}
