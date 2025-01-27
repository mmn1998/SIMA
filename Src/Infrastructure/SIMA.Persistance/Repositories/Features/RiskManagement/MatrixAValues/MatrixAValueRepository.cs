using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.MatrixAValues;

public class MatrixAValueRepository:Repository<MatrixAValue>, IMatrixAValueRepository
{
    private readonly SIMADBContext _context;

    public MatrixAValueRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<MatrixAValue> GetById(MatrixAValueId id)
    {
        return await _context.MatrixAValues.FirstOrDefaultAsync(c => c.Id == id) ?? throw SimaResultException.NotFound;
    }
}