using SIMA.Application.Query.Contract.Features.Logistics.Cartables;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Application.Query.Features.IssueManagement.Issues.Mappers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsRequest;

namespace SIMA.Application.Query.Features.Logistics.Cartables
{
    public class LogisticsRequestsQueryHandler :
    IQueryHandler<LogisticCartableGetQuery, Result<LogisticCartableGetQueryResult>>,
    IQueryHandler<GetMyLogisticCartableGetQuery, Result<GetMyLogisticCartableGetQueryResult>>,
    IQueryHandler<LogisticCartableGetAllQuery, Result<IEnumerable<LogisticCartablesGetAllQueryResult>>>,
     IQueryHandler<GetAllLogisticsRequestsQuery, Result<IEnumerable<LogisticCartablesGetAllQueryResult>>>
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
            var result = await _repository.GetLogesticCartableDetail(request.Id, request.IssueId);
            if (result.IssueInfo is not null)
                result.IssueInfo.WorkFlowFileContent =
                    result.IssueInfo.WorkFlowFileContent.ColorizeCurrentStep(result.IssueInfo.CurrentStepBpmnId);
            return Result.Ok(result);
        }
        public async Task<Result<IEnumerable<LogisticCartablesGetAllQueryResult>>> Handle(GetAllLogisticsRequestsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
        /// <summary>
        /// دریافت اطلاعات یک درخواست دارکات و خرید برای ثبت کننده آن
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<GetMyLogisticCartableGetQueryResult>> Handle(GetMyLogisticCartableGetQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetMyLogesticCartableDetail(request.Id, request.IssueId);
            if (result.IssueInfo is not null)
                result.IssueInfo.WorkFlowFileContent =
                    result.IssueInfo.WorkFlowFileContent.ColorizeCurrentStep(result.IssueInfo.CurrentStepBpmnId);
            return Result.Ok(result);
        }
    }
}
