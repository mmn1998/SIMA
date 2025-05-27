using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.DomainService.Features.RiskManagers.AffectedHistories;

public class AffectedHistoryDomainService : IAffectedHistoryDomainService
{
    private readonly SIMADBContext _context;

    public AffectedHistoryDomainService(SIMADBContext context)
    {
        _context = context;
    }

    public async Task CanBeDeleted(AffectedHistoryId id)
    {
        var predict = await _context.Severities.AnyAsync(x => x.AffectedHistoryId == id && x.SeverityValueId != null && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        if (predict)
        {
            throw new SimaResultException(CodeMessges._100117Code, Messages.AffectedHistoryAndConsequenceLevelAllocatedError);
        }
    }

    public async Task<bool> IsCodeUnique(string code, AffectedHistoryId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.AffectedHistories.AnyAsync(x => x.Code == code);
        else result = !await _context.AffectedHistories.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsNumericUnique(float value, AffectedHistoryId? id = null)
    {
        // این مورد که در دیتابیس یونیک نباشه ولی در برنامه یونیک بودن چک بشه باعث باگ خواهد شد و واحد فنی بک اند هیچ مسئولیتی در این مورد نخواهد پذیرفت و این موضوع به تیم تحلیل که درخواست این موضوع را داشت، اطلاع داده شد
        bool result = false;
        if (id == null) result = !await _context.AffectedHistories.AnyAsync(x => x.NumericValue == value && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        else result = !await _context.AffectedHistories.AnyAsync(x => x.NumericValue == value && x.Id != id && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        return result;
    }
}