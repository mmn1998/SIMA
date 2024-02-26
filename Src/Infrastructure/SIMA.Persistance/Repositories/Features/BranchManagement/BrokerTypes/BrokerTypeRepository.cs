using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.BrokerTypes
{
    public class BrokerTypeRepository : Repository<BrokerType>, IBrokerTypeRepository
    {
        private readonly SIMADBContext _context;
        public BrokerTypeRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<BrokerType> GetById(long id)
        {
            var stronglyTypeId = new BrokerTypeId(id);
            var entity = await _context.BrokerTypes.FirstOrDefaultAsync(pt => pt.Id == stronglyTypeId);
            entity.NullCheck();
            return entity;
        }
    }
}
