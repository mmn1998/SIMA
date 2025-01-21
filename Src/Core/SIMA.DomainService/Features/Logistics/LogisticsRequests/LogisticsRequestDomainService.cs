using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Contracts;
using SIMA.Framework.Common.Exceptions;
using SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsRequest;
using SIMA.Resources;

namespace SIMA.DomainService.Features.Logistics.LogisticsRequests;

public class LogisticsRequestDomainService : ILogisticsRequestDomainService
{
    private readonly ILogisticRequestQueryRepository _logisticRequestQueryRepository;
    private readonly IGoodsCategoryRepository _goodsCategoryRepository;

    public LogisticsRequestDomainService(ILogisticRequestQueryRepository logisticRequestQueryRepository , IGoodsCategoryRepository goodsCategoryRepository)
    {
        _logisticRequestQueryRepository = logisticRequestQueryRepository;
        _goodsCategoryRepository = goodsCategoryRepository;
    }

    public async Task IsCheckDurationGoodsCategory(List<CreateLogisticsRequestGoodsArg> args)
    {
        foreach (var arg in args) 
        {
            var goodsCategory = await _goodsCategoryRepository.GetById(new GoodsCategoryId(arg.GoodsCategoryId));
            if(goodsCategory.IsGoods == "0")
            {
                if (arg.ServiceDuration <= 0) throw new SimaResultException(CodeMessges._400Code, Messages.ServiceDuration);
            }
            if(goodsCategory.IsFixedAsset == "0")
            {
                if (arg.UsageDuration <= 0) throw new SimaResultException(CodeMessges._400Code, Messages.UsageDurationError);
            }
        }
    }

    public async Task<bool> IsGoods(List<long>? goodsId)
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
