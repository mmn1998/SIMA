using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Customers.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Contracts;
using SIMA.Framework.Common.Exceptions;
using SIMA.Persistance.Persistence;
using SIMA.Resources;
using System;
using System.Data.SqlClient;

namespace SIMA.DomainService.Features.TrustyDrafts.InquiryRequests;

public class InquiryRequestDomainService : IInquiryRequestDomainService
{
    private readonly SIMADBContext _context;
    private readonly string _connectionString;

    public InquiryRequestDomainService(SIMADBContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<long> GetBranchIdByUserId(long userId)
    {
        var query = @"
select b.Id from
Authentication.users U
Inner join Authentication.Profile P on U.ProfileID = P.Id
inner join Organization.Staff S on S.ProfileId = P.Id
inner join Organization.Position Po on po.Id = S.PositionId
inner join Bank.Branch B on B.Id = Po.BranchId
where U.Id = @UserId
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var response = await connection.QueryFirstOrDefaultAsync<long?>(query, new { UserId = userId }) ?? throw new SimaResultException(CodeMessges._100102Code, Messages.BranchIsNotDefinedWithUserId);
        return response;
    }

    public async Task<string?> GetLastRefrenceNumber()
    {
        var entity = await _context.InquiryRequests.OrderByDescending(c => c.CreatedAt).FirstOrDefaultAsync();
        return entity?.ReferenceNumber;
    }
    public async Task<string> GetBranchCodeByUserId(long userId)
    {
        var query = @"
select B.Code from
Authentication.users U
Inner join Authentication.Profile P on U.ProfileID = P.Id
inner join Organization.Staff S on S.ProfileId = P.Id
inner join Organization.Position Po on Po.Id = S.PositionId
inner join Bank.Branch B on B.Id = Po.BranchId
where U.Id = @UserId
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var response = await connection.QueryFirstOrDefaultAsync<string?>(query, new { UserId = userId }) ?? throw new SimaResultException(CodeMessges._100102Code, Messages.BranchIsNotDefinedWithUserId);
        return response.Length == 4 ? "0" + response : response;
    }

    public async Task<string?> GetCustomerNumber(long customerId)
    {
        var query = @"
select C.CustomerNumber from
Bank.Customer C
where C.Id = @CustomerId
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var response = await connection.QueryFirstOrDefaultAsync<string?>(query, new { CustomerId = customerId }) ?? throw new SimaResultException(CodeMessges._400Code, Messages.WrongCustomerError);
        return response.PadLeft(12, '0');
    }

    public async Task<string?> GetCurrencySymbol(long currencyTypeId)
    {
        var query = @"
select CT.Symbol from
Bank.CurrencyType CT
where CT.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var response = await connection.QueryFirstOrDefaultAsync<string?>(query, new { Id = currencyTypeId }) ?? throw new SimaResultException(CodeMessges._400Code, Messages.WrongCurrencyTypeError);
        return response.Substring(0, 2);
    }

    //public Task<bool> IsCodeUnique(string code, InquiryRequestId? id = null)
    //{

    //    throw new NotImplementedException();
    //}
}
