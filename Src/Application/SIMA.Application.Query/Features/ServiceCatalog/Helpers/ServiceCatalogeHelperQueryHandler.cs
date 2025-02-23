using SIMA.Application.Query.Contract.Features.Helpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Channels;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.CriticalActivities;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Products;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Services;

namespace SIMA.Application.Query.Features.ServiceCatalog.Helpers;

public class ServiceCatalogeHelperQueryHandler : IQueryHandler<GetLastCodeQuery, Result<GetLastCodeQueryResult>>
{
    private readonly IServiceQueryRepository _serviceQueryRepository;
    private readonly IProductQueryRepository _productQueryRepository;
    private readonly IChannelQueryRepository _channelQueryRepository;
    private readonly ICriticalActivitiyQueryRepository _criticalActivitiyQueryRepository;

    public ServiceCatalogeHelperQueryHandler(IServiceQueryRepository serviceQueryRepository, IProductQueryRepository productQueryRepository,
        IChannelQueryRepository channelQueryRepository, ICriticalActivitiyQueryRepository criticalActivitiyQueryRepository)
    {
        _serviceQueryRepository = serviceQueryRepository;
        _productQueryRepository = productQueryRepository;
        _channelQueryRepository = channelQueryRepository;
        _criticalActivitiyQueryRepository = criticalActivitiyQueryRepository;
    }
    public async Task<Result<GetLastCodeQueryResult>> Handle(GetLastCodeQuery request, CancellationToken cancellationToken)
    {
        var response = new GetLastCodeQueryResult();
        switch (request.Type?.ToLower())
        {
            case "se":
            {
                response.Code = await _serviceQueryRepository.GetLastCode();
                break;
            }
            case "ch":
            {
                response.Code = await _channelQueryRepository.GetLastCode();
                break;
            }
            case "pr":
            {
                response.Code = await _productQueryRepository.GetLastCode();
                break;
            }
            case "cr":
            {
                response.Code = await _criticalActivitiyQueryRepository.GetLastCode();
                break;
            }
            default:
                break;
        }
        return Result.Ok(response);
    }
}
