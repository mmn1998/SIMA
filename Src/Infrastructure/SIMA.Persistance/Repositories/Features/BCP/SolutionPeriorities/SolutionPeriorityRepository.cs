using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Entities;
using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.SolutionPeriorities;

//public class SolutionPeriorityRepository : Repository<SolutionPeriority>, ISolutionPeriorityRepository
//{
//    private readonly SIMADBContext _context;

//    public SolutionPeriorityRepository(SIMADBContext context) : base(context)
//    {
//        _context = context;
//    }

//    public async Task<SolutionPeriority> GetById(SolutionPeriorityId Id)
//    {
//        var entity = await _context.SolutionPeriorities.FirstOrDefaultAsync(x => x.Id == Id);
//        entity.NullCheck();
//        return entity ?? throw SimaResultException.NotFound;
//    }
//}