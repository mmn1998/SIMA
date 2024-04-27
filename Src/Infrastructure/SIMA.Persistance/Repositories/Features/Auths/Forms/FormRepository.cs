using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.Interfaces;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.Forms;

public class FormRepository : Repository<Form>, IFormRepository
{
    private readonly SIMADBContext _context;

    public FormRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Form> GetById(long Id)
    {
        var entity = await _context.Forms
            .Include(x => x.FormUsers)
            .Include(x => x.FormRoles)
            .Include(x => x.FormGroups)
            .FirstOrDefaultAsync(f => f.Id == new FormId(Id));
        entity.NullCheck();
        return entity;
    }
}
