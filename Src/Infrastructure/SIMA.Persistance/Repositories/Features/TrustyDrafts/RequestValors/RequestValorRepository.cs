using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.RequestValors;

public class RequestValorRepository : Repository<RequestValor>, IRequestValorRepository
{
    private readonly SIMADBContext _context;

    public RequestValorRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<RequestValor> GetById(RequestValorId Id)
    {
        var entity = await _context.RequestValors.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<RequestValor> GetByCode(string code)
    {
        var entity = await _context.RequestValors.FirstOrDefaultAsync(x => x.Code == code);
        return entity;
    }
}