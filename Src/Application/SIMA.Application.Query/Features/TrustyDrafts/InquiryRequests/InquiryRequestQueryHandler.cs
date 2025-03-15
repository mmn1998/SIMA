using SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryRequests;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Common.Helper.ExportHelpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.InquiryRequests;

namespace SIMA.Application.Query.Features.TrustyDrafts.InquiryRequests;

public class InquiryRequestQueryHandler : IQueryHandler<GetInquiryRequestQuery, Result<GetInquiryRequestQueryResult>>,
IQueryHandler<GetAllInquiryRequestsQuery, Result<IEnumerable<GetInquiryRequestQueryResult>>>
{
    private readonly IInquiryRequestQueryRepository _repository;
    private readonly ISimaReportService _simaReportService;

    public InquiryRequestQueryHandler(IInquiryRequestQueryRepository repository, ISimaReportService simaReportService)
    {
        _repository = repository;
        _simaReportService = simaReportService;
    }
    public async Task<Result<GetInquiryRequestQueryResult>> Handle(GetInquiryRequestQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetInquiryRequestQueryResult>>> Handle(GetAllInquiryRequestsQuery request, CancellationToken cancellationToken)
    {
        var res = await _repository.GetAll(request);
        if (!string.IsNullOrEmpty(request.FormatType))
        {
            res.Data = res.Data.Skip(request.Page).Take(request.PageSize);
            var excelByte = _simaReportService.ExportToExcel(res.Data);
            var exportResult = new ExportResult
            {
                Name = _simaReportService.GenerateFileName(this.GetType().Name.Replace("QueryHandler", "")) + "." + ExportExtensions.ExcelExtension,
                ContentType = ExportContentTypes.ExcelContentType,
                Extension = ExportExtensions.ExcelExtension,
                FileContent = excelByte,
            };
            res.ExportResult = exportResult;
        }
        return res;
    }
}
