using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.SolutionPeriorities;

//public class SolutionPeriorityDomainService : ISolutionPeriorityDomainService
//{
//    private readonly SIMADBContext _context;

//    public SolutionPeriorityDomainService(SIMADBContext context)
//    {
//        _context = context;
//    }
//    public async Task<bool> IsCodeUnique(string code, SolutionPeriorityId? Id = null)
//    {
//        bool result = false;
//        if (Id == null) result = !await _context.SolutionPeriorities.AnyAsync(x => x.Code == code);
//        else result = !await _context.SolutionPeriorities.AnyAsync(x => x.Code == code && x.Id != Id && x.ActiveStatusId != 3);
//        return result;
//    }
//}
