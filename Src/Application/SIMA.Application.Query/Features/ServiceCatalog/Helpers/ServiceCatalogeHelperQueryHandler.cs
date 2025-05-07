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
        var code = string.Empty;
        switch (request.Type?.ToLower())
        {
            case "se":
                {
                    code = await _serviceQueryRepository.GetLastCode();
                    break;
                }
            case "ch":
                {
                    code = await _channelQueryRepository.GetLastCode();
                    break;
                }
            case "pr":
                {
                    code = await _productQueryRepository.GetLastCode();
                    break;
                }
            case "cr":
                {
                    code = await _criticalActivitiyQueryRepository.GetLastCode();
                    break;
                }
            default:
                break;
        }
        response.Code = CalculateNext(code, request.Type);
        return Result.Ok(response);
    }
    private string CalculateNext(string lastCode, string? type)
    {
        var newCode = string.Empty;
        if (lastCode.Length == 5)
        {
            var prefix = lastCode.Substring(0, 2);
            var counter = lastCode.Substring(2, 3);
            var newCounter = (Convert.ToInt32(counter) + 1).ToString("000");
            newCode = $"{prefix}{newCounter}";
        }
        else
        {
            return $"{type?.ToUpper()}001";
        }
        return newCode;
    }
}
