using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Interfaces;
using SIMA.Persistance.Persistence;
using System.Text.RegularExpressions;

namespace SIMA.DomainService.Features.IssueManagement.IssueTypes;

public class IssueTypeDomainService : IIssueTypeDomainService
{
    private readonly SIMADBContext _context;

    public IssueTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.IssueTypes.AnyAsync(b => b.Code == code && b.Id != new IssueTypeId(id));
        else
            return !await _context.IssueTypes.AnyAsync(b => b.Code == code);
    }

    public bool IsHexCodeValid(string hexCode)
    {
        string hexCodePattern = @"[#][0-9A-Fa-f]{6}\b";
        return Regex.IsMatch(hexCode, hexCodePattern);
    }
}
