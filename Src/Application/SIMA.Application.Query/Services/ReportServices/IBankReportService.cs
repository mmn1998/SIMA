using SIMA.Application.Query.Contract.Features.DMS.Documents;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.ReferalLetters;

namespace SIMA.Application.Query.Services.ReportServices;

public interface IBankReportService
{
    GetDownloadDocumentQueryResult GenerateEceFile(GetReferalLetterQueryResult data);
}