using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.Branches;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.Branches;

public class BranchQueryRepository : IBranchQueryRepository
{
    private readonly string _connectionString;
    public BranchQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetBranchQueryResult>>> GetAll(GetAllBranchQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            if (!string.IsNullOrEmpty(request.Filter) && request.Filter.Contains(":"))
            {
                var splitedFilter = request.Filter.Split(":");
                string? SearchValue = splitedFilter[1].Trim().Sanitize().Sanitize();
                string filterClause = $"{splitedFilter[0].Trim()} Like N'%{SearchValue}%'";
                string queryCount = @$" 
                    SELECT COUNT(*)
                    FROM (
                        SELECT DISTINCT B.[Id]
                              ,B.[Name]
                              ,B.[Code]
                              ,B.[BranchTypeId]
                              ,B.[BranchChiefOfficerId]
                              ,B.ActiveStatusId
                              ,B.[BranchDeputyId]
                              ,B.[Latitude]
                              ,B.[Longitude]
                              ,B.[LocationId]
                              ,B.[PhoneNumber]
                              ,B.[PostalCode]
                              ,B.[Address]
                              ,B.[IsMultiCurrencyBranch]
                              ,S.[Name] as ActiveStatus
							  ,b.[CreatedAt]
                          FROM [Bank].[Branch] B
                          INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
                          WHERE  B.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT DISTINCT B.[Id]
                              ,B.[Name]
                              ,B.[Code]
                              ,B.[BranchTypeId]
                              ,B.[BranchChiefOfficerId]
                              ,B.ActiveStatusId
                              ,B.[BranchDeputyId]
                              ,B.[Latitude]
                              ,B.[Longitude]
                              ,B.[LocationId]
                              ,B.[PhoneNumber]
                              ,B.[PostalCode]
                              ,B.[Address]
                              ,B.[IsMultiCurrencyBranch]
                              ,S.[Name] as ActiveStatus
							  ,b.[CreatedAt]
                          FROM [Bank].[Branch] B
                          INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
                          WHERE  B.ActiveStatusId != 3
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
                    var response = await multi.ReadAsync<GetBranchQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                string queryCount = @" 
                SELECT Count(*) Result
                      FROM [Bank].[Branch] B
                      INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
                      WHERE  B.ActiveStatusId != 3
                      and (@SearchValue is null OR B.[Name] like @SearchValue or B.[Code] like @SearchValue) ";


                string query = $@"
                        SELECT DISTINCT B.[Id]
                              ,B.[Name]
                              ,B.[Code]
                              ,B.[BranchTypeId]
                              ,B.[BranchChiefOfficerId]
                              ,B.ActiveStatusId
                              ,B.[BranchDeputyId]
                              ,B.[Latitude]
                              ,B.[Longitude]
                              ,B.[LocationId]
                              ,B.[PhoneNumber]
                              ,B.[PostalCode]
                              ,B.[Address]
                              ,B.[IsMultiCurrencyBranch]
                              ,S.[Name] as ActiveStatus
							  ,b.[CreatedAt]
      FROM [Bank].[Branch] B
      INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
      WHERE  B.ActiveStatusId != 3
      and (@SearchValue is null OR B.[Name] like @SearchValue or B.[Code] like @SearchValue)
      order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
      OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";

                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetBranchQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }

    public async Task<GetBranchQueryResult> GetById(long id)
    {
        string query = string.Empty;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            query = @"
                       SELECT DISTINCT B.[Id]
                                     ,B.[Name]
                                     ,B.[Code]
                                     ,B.[BranchTypeId]
                                     ,B.[BranchChiefOfficerId]
                                     ,B.ActiveStatusId
                                     ,B.[BranchDeputyId]
                                     ,B.[Latitude]
                                     ,B.[Longitude]
                                     ,B.[LocationId]
                                     ,B.[PhoneNumber]
                                     ,B.[PostalCode]
                                     ,B.[Address]
                                     ,B.[IsMultiCurrencyBranch]
                                   , S.[Name] as ActiveStatus
                                 FROM [Bank].[Branch] B
                         INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
                         WHERE B.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetBranchQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.NullException;

            return result;
        }

    }
}

       
       
