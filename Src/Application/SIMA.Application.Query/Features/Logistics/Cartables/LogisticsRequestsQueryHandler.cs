using SIMA.Application.Query.Contract.Features.Logistics.Cartables;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsRequest;

namespace SIMA.Application.Query.Features.Logistics.Cartables
{
    public class LogisticsRequestsQueryHandler :
    IQueryHandler<LogisticCartableGetQuery, Result<LogisticCartableGetQueryResult>>,
    IQueryHandler<LogisticCartableGetAllQuery, Result<IEnumerable<LogisticCartablesGetAllQueryResult>>>
    {
        private readonly ILogisticRequestQueryRepository _repository;

        public LogisticsRequestsQueryHandler(ILogisticRequestQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetLogisticRequestsQueryResult>> Handle(GetLogisticRequestsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<LogisticCartablesGetAllQueryResult>>> Handle(LogisticCartableGetAllQuery request, CancellationToken cancellationToken)
        {           
            return await _repository.GetLogesticCartables(request);
        }

        public async Task<Result<LogisticCartableGetQueryResult>> Handle(LogisticCartableGetQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetLogesticCartableDetail(request.Id , request.IssueId);
        }
    }
}
