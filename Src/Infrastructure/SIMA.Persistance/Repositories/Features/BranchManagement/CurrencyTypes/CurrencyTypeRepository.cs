using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.CurrencyTypes
{
    public class CurrencyTypeRepository : Repository<CurrencyType>, ICurrencyTypeRepository
    {
        private readonly SIMADBContext _context;

        public CurrencyTypeRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CurrencyType> GetById(long id)
        {
            var stronglyTypeId = new CurrencyTypeId(id);
            var entity = await _context.CurrencyTypes.FirstOrDefaultAsync(pt => pt.Id == stronglyTypeId);
            entity.NullCheck();
            return entity;
        }
    }
}
