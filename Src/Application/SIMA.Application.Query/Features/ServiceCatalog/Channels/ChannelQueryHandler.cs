using SIMA.Application.Query.Contract.Features.ServiceCatalog.Channels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Channels;

namespace SIMA.Application.Query.Features.ServiceCatalog.Channels;

public class ChannelQueryHandler : IQueryHandler<GetChannelQuery, Result<GetChannelQueryResult>>,
    IQueryHandler<GetAllChannelsQuery, Result<IEnumerable<GetChannelQueryResult>>>
{
    private readonly IChannelQueryRepository _repository;

    public ChannelQueryHandler(IChannelQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetChannelQueryResult>> Handle(GetChannelQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetChannelQueryResult>>> Handle(GetAllChannelsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
