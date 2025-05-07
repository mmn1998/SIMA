using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Contracts;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.ConsequenceValues;

public class ConsequenceValueRepository : Repository<ConsequenceValue>, IConsequenceValueRepository
{
    private readonly SIMADBContext _context;

    public ConsequenceValueRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ConsequenceValue> GetById(ConsequenceValueId Id)
    {
        var entity = await _context.ConsequenceValues.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}