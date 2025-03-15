using SIMA.Application.Query.Contract.Features.Logistics.Cartables;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Common.Helper.ExportHelpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsRequest;

namespace SIMA.Application.Query.Features.Logistics.Cartables
{
    public class LogisticsRequestsQueryHandler :
      IQueryHandler<LogisticCartableGetQuery, Result<LogisticCartableGetQueryResult>>,
      IQueryHandler<LogisticCartableGetAllQuery, Result<IEnumerable<LogisticCartablesGetAllQueryResult>>>,
      IQueryHandler<GetAllLogisticsRequestsQuery, Result<IEnumerable<LogisticCartablesGetAllQueryResult>>>
    {
        private readonly ILogisticRequestQueryRepository _repository;
        private readonly ISimaReportService _reportService;

        public LogisticsRequestsQueryHandler(ILogisticRequestQueryRepository repository, ISimaReportService reportService)
        {
            _repository = repository;
            _reportService = reportService;
        }

        public async Task<Result<GetLogisticRequestsQueryResult>> Handle(GetLogisticRequestsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<LogisticCartablesGetAllQueryResult>>> Handle(LogisticCartableGetAllQuery request, CancellationToken cancellationToken)
        {
            var res = await _repository.GetLogesticCartables(request);
            if (!string.IsNullOrEmpty(request.FormatType))
            {
                var excelByte = _reportService.ExportToExcel(res.Data);
                res.Data = res.Data.Skip(request.Page).Take(request.PageSize);
                var exportResult = new ExportResult
                {
                    Name = _reportService.GenerateFileName(this.GetType().Name.Replace("QueryHandler", "")) + "." + ExportExtensions.ExcelExtension,
                    ContentType = ExportContentTypes.ExcelContentType,
                    Extension = ExportExtensions.ExcelExtension,
                    FileContent = excelByte,
                };
                res.ExportResult = exportResult;
            }
            return res;
        }

        public async Task<Result<LogisticCartableGetQueryResult>> Handle(LogisticCartableGetQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetLogesticCartableDetail(request.Id, request.IssueId);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<LogisticCartablesGetAllQueryResult>>> Handle(GetAllLogisticsRequestsQuery request, CancellationToken cancellationToken)
        {
            var res = await _repository.GetAll(request);
            if (!string.IsNullOrEmpty(request.FormatType))
            {
                var excelByte = _reportService.ExportToExcel(res.Data);
                res.Data = res.Data.Skip(request.Page).Take(request.PageSize);
                var exportResult = new ExportResult
                {
                    Name = _reportService.GenerateFileName(this.GetType().Name.Replace("QueryHandler", "")) + "." + ExportExtensions.ExcelExtension,
                    ContentType = ExportContentTypes.ExcelContentType,
                    Extension = ExportExtensions.ExcelExtension,
                    FileContent = excelByte,
                };
                res.ExportResult = exportResult;
            }

            return res;
        }
    }
}
