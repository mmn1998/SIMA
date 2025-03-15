using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.InherentOccurrenceProbabilityValues;

public class InherentOccurrenceProbabilityValueDomainService : IInherentOccurrenceProbabilityValueDomainService
{
    private readonly SIMADBContext _context;

    public InherentOccurrenceProbabilityValueDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, InherentOccurrenceProbabilityValueId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.InherentOccurrenceProbabilityValues.AnyAsync(x => x.Code == code);
        else result = !await _context.InherentOccurrenceProbabilityValues.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsNumericUnique(float value, InherentOccurrenceProbabilityValueId? id = null)
    {
        // این مورد که در دیتابیس یونیک نباشه ولی در برنامه یونیک بودن چک بشه باعث باگ خواهد شد و واحد فنی بک اند هیچ مسئولیتی در این مورد نخواهد پذیرفت و این موضوع به تیم تحلیل که درخواست این موضوع را داشت، اطلاع داده شد
        bool result = false;
        if (id == null) result = !await _context.InherentOccurrenceProbabilityValues.AnyAsync(x => x.NumericValue == value && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        else result = !await _context.InherentOccurrenceProbabilityValues.AnyAsync(x => x.NumericValue == value && x.Id != id && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        return result;
    }
}