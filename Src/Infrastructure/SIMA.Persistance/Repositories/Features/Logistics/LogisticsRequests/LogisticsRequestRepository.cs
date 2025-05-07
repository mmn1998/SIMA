using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Contracts;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Logistics.LogisticsRequests
{
    public class LogisticsRequestRepository : Repository<LogisticsRequest>, ILogisticsRequestRepository
    {
        private readonly SIMADBContext _context;

        public LogisticsRequestRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LogisticsRequest> GetById(long Id)
        {
            var entity = await _context.LogisticsRequests.Include(x => x.LogisticsRequestGoods).Include(x => x.LogisticsRequestDocuments).FirstOrDefaultAsync(x => x.Id == new LogisticsRequestId(Id));
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }

        public async Task<LogisticsRequest> GetByLogisticsRequestGoodsId(long requestGoodsId)
        {
            var entity = await _context.LogisticsRequests
            .Include(x => x.LogisticsRequestGoods)
            .FirstOrDefaultAsync(lr => lr.LogisticsRequestGoods.Any(s => s.Id == new LogisticsRequestGoodsId(requestGoodsId)));


            return entity;
        }

        public async Task<LogisticsRequest> GetLastLogisticsRequest()
        {
            var entity = await _context.LogisticsRequests.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
            return entity;
        }

    }
}
