using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.ValueObjects;
using SIMA.Persistance.Persistence;
using System.Data.SqlClient;

namespace SIMA.DomainService.Features.TrustyDrafts.InquiryResponses;

public class InquiryResponseDomainService : IInquiryResponseDomainService
{
    private readonly SIMADBContext _context;
    private readonly string _connectionString;

    public InquiryResponseDomainService(SIMADBContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<bool> CurrencyTypeIdEquals(long wageRateId, long inquiryRequestCurrencyId)
    {
        var query = @"
select WR.CurrencyTypeId from
TrustyDraft.WageRate WR
where WR.Id = @Id
";
        var secondQuery = @"
select IRC.CurrencyTypeId from
TrustyDraft.InquiryRequestCurrency IRC
where IRC.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var wageRateCurrencyTypeId = await connection.QueryFirstOrDefaultAsync<long>(query, new { Id = wageRateId });
        var currencyTypeId = await connection.QueryFirstOrDefaultAsync<long>(secondQuery, new { Id = inquiryRequestCurrencyId });
        return wageRateCurrencyTypeId == currencyTypeId;
    }

    public Task<bool> IsCodeUnique(string code, InquiryResponseId? id = null)
    {
        throw new NotImplementedException();
    }
}
