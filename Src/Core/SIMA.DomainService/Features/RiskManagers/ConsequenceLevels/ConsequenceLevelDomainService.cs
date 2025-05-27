using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.DomainService.Features.RiskManagers.ConsequenceLevels;

public class ConsequenceLevelDomainService : IConsequenceLevelDomainService
{
    private readonly SIMADBContext _context;

    public ConsequenceLevelDomainService(SIMADBContext context)
    {
        _context = context;
    }

    public async Task CanBeDeleted(ConsequenceLevelId id)
    {
        var predict = await _context.Severities.AnyAsync(x => x.ConsequenceLevelId == id && x.SeverityValueId != null && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        if (predict)
        {
            throw new SimaResultException(CodeMessges._100117Code, Messages.AffectedHistoryAndConsequenceLevelAllocatedError);
        }
    }

    public async Task<bool> IsCodeUnique(string code, ConsequenceLevelId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.ConsequenceLevels.AnyAsync(x => x.Code == code);
        else result = !await _context.ConsequenceLevels.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsNumericUnique(float value, ConsequenceLevelId? id = null)
    {
        // این مورد که در دیتابیس یونیک نباشه ولی در برنامه یونیک بودن چک بشه باعث باگ خواهد شد و واحد فنی بک اند هیچ مسئولیتی در این مورد نخواهد پذیرفت و این موضوع به تیم تحلیل که درخواست این موضوع را داشت، اطلاع داده شد
        bool result = false;
        if (id == null) result = !await _context.ConsequenceLevels.AnyAsync(x => x.NumericValue == value && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        else result = !await _context.ConsequenceLevels.AnyAsync(x => x.NumericValue == value && x.Id != id && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        return result;
    }
}

