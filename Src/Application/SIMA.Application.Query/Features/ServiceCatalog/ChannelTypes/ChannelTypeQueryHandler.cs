using SIMA.Application.Query.Contract.Features.ServiceCatalog.ChannelTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ChannelTypes;

namespace SIMA.Application.Query.Features.ServiceCatalog.ChannelTypes;

public class ChannelTypeQueryHandler : IQueryHandler<GetChannelTypeQuery, Result<GetChannelTypeQueryResult>>,
    IQueryHandler<GetAllChannelTypeQuery, Result<IEnumerable<GetChannelTypeQueryResult>>>
{
    private readonly IChannelTypeQueryRepository _repository;

    public ChannelTypeQueryHandler(IChannelTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetChannelTypeQueryResult>> Handle(GetChannelTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetChannelTypeQueryResult>>> Handle(GetAllChannelTypeQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}