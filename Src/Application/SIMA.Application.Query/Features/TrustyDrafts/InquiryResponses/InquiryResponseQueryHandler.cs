using SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryResponses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.InquiryResponses;

namespace SIMA.Application.Query.Features.TrustyDrafts.InquiryResponses;

public class InquiryResponseQueryHandler : IQueryHandler<GetInquiryResponseQuery, Result<GetInquiryResponseQueryResult>>,
IQueryHandler<GetAllInquiryResponsesQuery, Result<IEnumerable<GetInquiryResponseQueryResult>>>,
IQueryHandler<GetAllAcceptInqueryResponseQuery, Result<IEnumerable<GetInquiryResponseQueryResult>>>
{
    private readonly IInquiryResponseQueryRepository _repository;

    public InquiryResponseQueryHandler(IInquiryResponseQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetInquiryResponseQueryResult>> Handle(GetInquiryResponseQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetInquiryResponseQueryResult>>> Handle(GetAllInquiryResponsesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<IEnumerable<GetInquiryResponseQueryResult>>> Handle(GetAllAcceptInqueryResponseQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAcceptResponse();
    }
}
