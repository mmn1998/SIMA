using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueLinkReasons;

namespace SIMA.Application.Query.Features.IssueManagement.IssueLinkReasons
{
    public class IssueLinkReasonQueryHandler : IQueryHandler<GetAllIssueLinkReasonsQuery, Result<List<GetIssueLinkReasonQueryResult>>>,
    IQueryHandler<GetIssueLinkReasonQuery, Result<GetIssueLinkReasonQueryResult>>
    {
        private readonly IIssueLinkReasonQueryRepository _repository;

        public IssueLinkReasonQueryHandler(IIssueLinkReasonQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<List<GetIssueLinkReasonQueryResult>>> Handle(GetAllIssueLinkReasonsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request.Request);
        }

        public async Task<Result<GetIssueLinkReasonQueryResult>> Handle(GetIssueLinkReasonQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.FindById(request.Id);
            return Result.Ok(result);
        }
    }
}
