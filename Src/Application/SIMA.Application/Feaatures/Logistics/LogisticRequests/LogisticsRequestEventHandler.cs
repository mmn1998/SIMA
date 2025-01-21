using AutoMapper;
using MediatR;
using Sima.Framework.Core.Repository;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Contracts;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Events;
using SIMA.Framework.Common.Security;

namespace SIMA.Application.Feaatures.Logistics.LogisticRequests
{
    public class LogisticsRequestEventHandler : INotificationHandler<ChangeGoodsStatusEvent>
    {

        private readonly IMapper _mapper;
        private readonly ISimaIdentity _simaIdentity;
        private readonly ILogisticsRequestRepository _logisticsRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogisticsRequestEventHandler(IMapper mapper,ISimaIdentity simaIdentity , ILogisticsRequestRepository logisticsRequestRepository , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _simaIdentity = simaIdentity;
            _logisticsRequestRepository = logisticsRequestRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(ChangeGoodsStatusEvent notification, CancellationToken cancellationToken)
        {
            foreach (var item in notification.logisticeRequestGoods)
            {
                try
                {
                    var entity = await _logisticsRequestRepository.GetByLogisticsRequestGoodsId(item);
                    await entity.ChangeGoodsStatus(item, notification.GoodsStatus);
                }
                catch (Exception ex)
                {

                    throw;
                }
               
            }
        }
    }
}
