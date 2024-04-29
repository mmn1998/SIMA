using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.AddressTypes;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.AddressTypes;

public class AddressTypeQueryRepository : IAddressTypeQueryRepository
{
    private readonly string _connectionString;

    public AddressTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetAddressTypeQueryResult> FindById(long id)
    {
        var query = @"SELECT 
                           at.[ID] Id
                          ,at.[Name]
                          ,at.[Code]
                          ,at.[ActiveStatusID]
                          ,at.[CreatedAt]
                          ,at.[CreatedBy]
                          ,at.[ModifiedAt]
                          ,at.[ModifiedBy],
                          a.ID ActiveStatusId, 
                          a.Name ActiveStatus 
                      FROM [Basic].[AddressType] at
                             join Basic.ActiveStatus a
                             on at.ActiveStatusId = a.ID
                      WHERE at.Id = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<GetAddressTypeQueryResult>(query, new { Id = id });
            return result ?? throw SimaResultException.NotFound;
        }


    }

    public async Task<Result<List<GetAddressTypeQueryResult>>> GetAll(GetAllAddressTypesQuery request)
    {
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var queryCount = @" SELECT  COUNT(*) Result
                                FROM [Basic].[AddressType]  at
                                join Basic.ActiveStatus a
                                on at.ActiveStatusId = a.ID
                               WHERE (@SearchValue is null OR (at.Name like @SearchValue OR at.Code like @SearchValue)) AND at.[ActiveStatusID] <> 3";

            string query = $@"
                                 SELECT DISTINCT 
                                    at.[ID] as Id
                                   ,at.[Name]
                                   ,at.[Code]
                                   ,a.ID ActiveStatusId
                                   ,a.Name ActiveStatus
                                   ,at.[CreatedAt]
                               FROM [Basic].[AddressType]  at
                                    join Basic.ActiveStatus a
                                    on at.ActiveStatusId = a.ID
                                WHERE (@SearchValue is null OR (at.Name like @SearchValue OR at.Code like @SearchValue)) AND at.[ActiveStatusID] <> 3
                                order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                request.Skip,
                request.PageSize
            }))
            {
                var response = (await multi.ReadAsync<GetAddressTypeQueryResult>()).ToList();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
    }
    public async Task<List<GetAddressTypeQueryResult>> GetAllForRedis()
    {

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = $@"
                                 SELECT DISTINCT 
                                    at.[ID] as Id
                                   ,at.[Name]
                                   ,at.[Code]
                                   ,a.ID ActiveStatusId
                                   ,a.Name ActiveStatus
                                   ,at.[CreatedAt]
                               FROM [Basic].[AddressType]  at
                                    join Basic.ActiveStatus a
                                    on at.ActiveStatusId = a.ID
                                WHERE  at.[ActiveStatusID] <> 3 ;";
            var result = (await connection.QueryAsync<GetAddressTypeQueryResult>(query)).ToList();
            return result;
        }
    }
}
