using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.Contracts;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.ConsequenceIntensionDescriptions;

public class ConsequenceIntensionDescriptionRepository : Repository<ConsequenceIntensionDescription>, IConsequenceIntensionDescriptionRepository
{
    private readonly SIMADBContext _context;

    public ConsequenceIntensionDescriptionRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ConsequenceIntensionDescription> GetById(ConsequenceIntensionDescriptionId Id)
    {
        var entity = await _context.ConsequenceIntensionDescriptions.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}