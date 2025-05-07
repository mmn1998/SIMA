using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Contracts;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Entities;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.ImportanceDegrees;

public class ImportanceDegreeRepository : Repository<ImportanceDegree>, IImportanceDegreeRepository
{
    private readonly SIMADBContext _context;

    public ImportanceDegreeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ImportanceDegree> GetById(ImportanceDegreeId Id)
    {
        var entity = await _context.ImportanceDegrees.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}