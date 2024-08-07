using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Contracts;
using SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsRequest;

namespace SIMA.DomainService.Features.Logistics.LogisticsRequests;

public class LogisticsRequestDomainService : ILogisticsRequestDomainService
{
    private readonly ILogisticRequestQueryRepository _logisticRequestQueryRepository;

    public LogisticsRequestDomainService(ILogisticRequestQueryRepository logisticRequestQueryRepository)
    {
        _logisticRequestQueryRepository = logisticRequestQueryRepository;
    }
    public async Task<bool> IsGoods(List<long> goodsId)
    {
        return await _logisticRequestQueryRepository.IsGoods(goodsId);
    }

    public async Task<bool> IsHardware(List<long> goodsId)
    {
        return await _logisticRequestQueryRepository.IsHardware(goodsId);
    }

    public async Task<bool> IsTechnological(List<long> goodsId)
    {
        return await _logisticRequestQueryRepository.IsTechnological(goodsId);
    }
}
