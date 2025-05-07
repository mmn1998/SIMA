using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.Frequencies;

public class FrequencyRepository: Repository<Frequency>, IFrequencyRepository
{
    private readonly SIMADBContext _context;

    public FrequencyRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Frequency> GetById(FrequencyId id)
    {
        var entity = await _context.Frequencies.FirstOrDefaultAsync(x => x.Id == id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}