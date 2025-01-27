using SIMA.Application.Query.Contract.Features.DMS.Documents;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.ReferalLetters;
using SIMA.Application.Query.Services.ReportServices;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.ReferralLetters;

namespace SIMA.Application.Query.Features.TrustyDrafts.ReferralLetters;

public class ReferralLetterQueryHandler :
IQueryHandler<GetAllReferalLettersQuery, Result<IEnumerable<GetAllReferralLettersQueryResult>>>,
IQueryHandler<GetAllReferralLetterToSecretariatQuery, Result<IEnumerable<GetAllReferralLettersQueryResult>>>,
IQueryHandler<GetAllReferralLetterToExchangeQuery, Result<IEnumerable<GetAllReferralLettersQueryResult>>>,
IQueryHandler<GetReferalLetterQuery, Result<GetReferalLetterQueryResult>>,
    IQueryHandler<GetReferalLetterQueryByLetterNumber, Result<GetDownloadDocumentQueryResult>>
{
    private readonly IReferralLetterQueryRepository _repository;
    private readonly IBankReportService _reportService;

    public ReferralLetterQueryHandler(IReferralLetterQueryRepository repository, IBankReportService reportService)
    {
        _repository = repository;
        _reportService = reportService;
    }
    public async Task<Result<IEnumerable<GetAllReferralLettersQueryResult>>> Handle(GetAllReferalLettersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
    public async Task<Result<IEnumerable<GetAllReferralLettersQueryResult>>> Handle(GetAllReferralLetterToSecretariatQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllToSecretariat(request);
    }
    public async Task<Result<GetReferalLetterQueryResult>> Handle(GetReferalLetterQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<GetDownloadDocumentQueryResult>> Handle(GetReferalLetterQueryByLetterNumber request, CancellationToken cancellationToken)
    {
        var dbResult = await _repository.GetByLetterNumber(request.LetterNumber);
        var reportResult = _reportService.GenerateEceFile(dbResult);
        return Result.Ok(reportResult);
    }

    public async Task<Result<IEnumerable<GetAllReferralLettersQueryResult>>> Handle(GetAllReferralLetterToExchangeQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllToExchange(request);
    }
}
