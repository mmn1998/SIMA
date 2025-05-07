using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.IssueManagement;

public class IssueWeightCategoryRepository : Repository<IssueWeightCategory>, IIssueWeightCategoryRepository
{
    private readonly SIMADBContext _context;

    public IssueWeightCategoryRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IssueWeightCategory> GetById(long id)
    {
        var entity = await _context.IssueWeightCategories.FirstOrDefaultAsync(iwc => iwc.Id == new IssueWeightCategoryId(id));
        entity.NullCheck();
        return entity;
    }
}
