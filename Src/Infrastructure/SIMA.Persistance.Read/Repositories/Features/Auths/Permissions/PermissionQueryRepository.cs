using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Permission;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.BranchManagement.Branches;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Permissions;

public class PermissionQueryRepository : IPermissionQueryRepository
{
    private readonly string _connectionString;

    public PermissionQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetPermissionQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                SELECT DISTINCT P.[Id] 
                      ,P.[Name]
                      ,P.[Code]
                      ,P.[ActiveStatusID]
                      ,A.[Name] as ActiveStatus 
                  FROM [Authentication].[Permission] P 
                  join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                  WHERE P.Id = @Id and P.[ActiveStatusID] != 3 ";
            var result = await connection.QueryFirstOrDefaultAsync<GetPermissionQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }

    public async Task<Result<IEnumerable<GetPermissionQueryResult>>> GetAll(GetAllPermissionsByDomainIdQuery request)
    {
        try
        {            
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                if (!string.IsNullOrEmpty(request.Filter) && request.Filter.Contains(":"))
                {
                    var splitedFilter = request.Filter.Split(":");
                    string? SearchValue = splitedFilter[1].Trim().Sanitize();
                    string filterClause = $"{splitedFilter[0].Trim()} Like N'%{SearchValue}%'";
                    string queryCount = @$" 
                    SELECT COUNT(*)
                    FROM (
                        SELECT DISTINCT P.[Id] 
                         ,P.[Name]
                             ,P.[Code]
                             ,P.[ActiveStatusID]
                             ,A.[Name] as ActiveStatus 
                             ,p.[CreatedAt]
                       FROM [Authentication].[Permission] P 
                       join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                       WHERE  P.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause};";
                    string query = $@"
                    SELECT *
                    FROM (
                        SELECT DISTINCT P.[Id] 
                             ,P.[Name]
                             ,P.[Code]
                             ,P.[ActiveStatusID]
                             ,A.[Name] as ActiveStatus 
                             ,p.[CreatedAt]
                       FROM [Authentication].[Permission] P 
                       join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                   WHERE  P.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause}
                    ORDER BY {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
                    using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                    {
                        request.Skip,
                        request.PageSize
                    }))
                    {
                        var response = await multi.ReadAsync<GetPermissionQueryResult>();
                        var count = await multi.ReadSingleAsync<int>();
                        return Result.Ok(response, count, request.PageSize, request.Page);
                    }
                }
                else
                {
                    string queryCount = @"
                   SELECT COUNT(*) Result
                   FROM [Authentication].[Permission] P 
                   join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                   WHERE  P.ActiveStatusId != 3
                   and (@SearchValue is null OR P.[Name] like @SearchValue or P.[Code] like @SearchValue)";
                    string query = $@"
                   SELECT DISTINCT P.[Id] 
                         ,P.[Name]
                         ,P.[Code]
                         ,P.[ActiveStatusID]
                         ,A.[Name] as ActiveStatus 
                         ,p.[CreatedAt]
                   FROM [Authentication].[Permission] P 
                   join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                   WHERE  P.ActiveStatusId != 3
                   and (@SearchValue is null OR P.[Name] like @SearchValue or P.[Code] like @SearchValue)
                    order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                   OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

                    using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                    {
                        SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                        request.Skip,
                        request.PageSize
                    }))
                    {
                        var response = await multi.ReadAsync<GetPermissionQueryResult>();
                        var count = await multi.ReadSingleAsync<int>();
                        return Result.Ok(response, count, request.PageSize, request.Page);
                    }
                }
            }
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public async Task<Result<IEnumerable<GetPermissionQueryResult>>> GetAll(GetAllPermissionsByDomainIdQuery request,long domainId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {

            string queryCount = @"
                   SELECT COUNT(*) Result
                   FROM [Authentication].[Permission] P 
                   join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                    WHERE  P.[ActiveStatusID] != 3 and P.[DomainId] = @DomainId 
                   and (@SearchValue is null OR P.[Name] like @SearchValue or P.[Code] like @SearchValue)";
            await connection.OpenAsync();
            string query = $@"
                   SELECT DISTINCT P.[Id] 
                         ,P.[Name]
                         ,P.[Code]
                         ,P.[ActiveStatusID]
                         ,A.[Name] as ActiveStatus 
                         ,p.[CreatedAt]
                   FROM [Authentication].[Permission] P 
                   join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                    WHERE  P.[ActiveStatusID] != 3 and P.[DomainId] = @DomainId 
                   and (@SearchValue is null OR P.[Name] like @SearchValue or P.[Code] like @SearchValue)
                   order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                   OFFSET @Skip rows FETCH NEXT @PageSize rows only;";



            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                request.Skip,
                request.PageSize,
                DomainId = domainId
            }))
            {
                var response = await multi.ReadAsync<GetPermissionQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
    }
}