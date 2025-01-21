using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Enitites;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.CurrencyOprationTypes
{
    public class CurrencyOprationTypeRepository : Repository<CurrencyOprationType>, ICurrencyOprationTypeRepository
    {
        private readonly SIMADBContext _context;

        public CurrencyOprationTypeRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CurrencyOprationType> GetById(long id)
        {
            var stronglyTypeId = new CurrencyOprationTypeId(id);
            var entity = await _context.CurrencyOprationTypes.FirstOrDefaultAsync(pt => pt.Id == stronglyTypeId);
            entity.NullCheck();
            return entity;
        }
    }
}
