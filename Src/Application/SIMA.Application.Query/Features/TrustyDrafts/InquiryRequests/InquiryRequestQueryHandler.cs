using SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryRequests;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.InquiryRequests;

namespace SIMA.Application.Query.Features.TrustyDrafts.InquiryRequests;

public class InquiryRequestQueryHandler : IQueryHandler<GetInquiryRequestQuery, Result<GetInquiryRequestQueryResult>>,
IQueryHandler<GetAllInquiryRequestsQuery, Result<IEnumerable<GetInquiryRequestQueryResult>>>
{
    private readonly IInquiryRequestQueryRepository _repository;

    public InquiryRequestQueryHandler(IInquiryRequestQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetInquiryRequestQueryResult>> Handle(GetInquiryRequestQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetInquiryRequestQueryResult>>> Handle(GetAllInquiryRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
