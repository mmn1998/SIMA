using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Entities;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.MatrixAs;

public class MatrixARepository : Repository<MatrixA>, IMatrixARepository
{
    private readonly SIMADBContext _context;

    public MatrixARepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<MatrixA> GetById(MatrixAId id)
    {
        return await _context.MatrixAs.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}