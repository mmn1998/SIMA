using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Contracts;
using SIMA.Framework.Common.Exceptions;
using SIMA.Persistance.Persistence;
using SIMA.Resources;
using System.Text.RegularExpressions;

namespace SIMA.DomainService.Features.ServiceCatalog.Services;

public class ServiceDomainService : IServiceDomainService
{
    private readonly SIMADBContext _context;

    public ServiceDomainService(SIMADBContext context)
    {
        _context = context;
    }

    public void CheckValidURL(string url)
    {
        var pattern = @"^https?:\/\/[a-zA-Z0-9\-.]+\.[a-zA-Z]{2,}(\/\S*)?$";
        var isMatch = Regex.IsMatch(url, pattern);
        if (!isMatch) throw new SimaResultException(CodeMessges._400Code, Messages.UrlError);
    }

    public async Task<bool> IsCodeUnique(string code, ServiceId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Services.AnyAsync(x => x.Code == code);
        else result = !await _context.Services.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}