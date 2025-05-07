using SIMA.Application.Query.Contract.Features.TrustyDrafts.BrokerInquiryStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.BrokerInquiryStatuses;

namespace SIMA.Application.Query.Features.TrustyDrafts.BrokerInquiryStatuses;

public class BrokerInquiryStatusQueryHandler : IQueryHandler<GetBrokerInquiryStatusQuery, Result<GetBrokerInquiryStatusQueryResult>>,
IQueryHandler<GetAllBrokerInquiryStatusesQuery, Result<IEnumerable<GetBrokerInquiryStatusQueryResult>>>
{
    private readonly IBrokerInquiryStatusQueryRepository _repository;

    public BrokerInquiryStatusQueryHandler(IBrokerInquiryStatusQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetBrokerInquiryStatusQueryResult>> Handle(GetBrokerInquiryStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetBrokerInquiryStatusQueryResult>>> Handle(GetAllBrokerInquiryStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}