using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueLinkReasons;

public class IssueLinkReasonQueryRepository : IIssueLinkReasonQueryRepository
{
    private readonly string _connectionString;

    public IssueLinkReasonQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetIssueLinkReasonQueryResult> FindById(long id)
    {
        var query = $@"SELECT ILR.[ID]
                          ,ILR.[Name]
                          ,ILR.[Code]
                          ,ILR.[ActiveStatusID]
                          ,A.[Name] as ActiveStatus
                      FROM [ISSUEMANAGEMENT].[IssueLinkReason] ILR
                      INNER JOIN [Basic].[ActiveStatus] A on A.ID = ILR.ActiveStatusId
                      WHERE ILR.Id = @Id and ILR.ActiveStatusId != 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<GetIssueLinkReasonQueryResult>(query, new { Id = id });
            return result ?? throw SimaResultException.NotFound;
        }
    }

    public async Task<Result<IEnumerable<GetIssueLinkReasonQueryResult>>> GetAll(GetAllIssueLinkReasonsQuery request )
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
                        SELECT DISTINCT
                                     ILR.[ID]
                                    ,ILR.[Name]
                                    ,ILR.[Code]
                                    ,ILR.[ActiveStatusID]
                                    ,A.[Name] as ActiveStatus
                                    ,ILR.[CreatedAt]
                                    FROM [ISSUEMANAGEMENT].[IssueLinkReason] ILR
                        INNER JOIN [Basic].[ActiveStatus] A on A.ID = ILR.ActiveStatusId
                        WHERE ILR.[ActiveStatusID] <> 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT DISTINCT
                                     ILR.[ID]
                                    ,ILR.[Name]
                                    ,ILR.[Code]
                                    ,ILR.[ActiveStatusID]
                                    ,A.[Name] as ActiveStatus
                                    ,ILR.[CreatedAt]
                                    FROM [ISSUEMANAGEMENT].[IssueLinkReason] ILR
                        INNER JOIN [Basic].[ActiveStatus] A on A.ID = ILR.ActiveStatusId
                        WHERE ILR.[ActiveStatusID] <> 3
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
                    var response = await multi.ReadAsync<GetIssueLinkReasonQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                var queryCount = @" SELECT  COUNT(*) Result
                                    FROM [ISSUEMANAGEMENT].[IssueLinkReason] ILR
                                    INNER JOIN [Basic].[ActiveStatus] A on A.ID = ILR.ActiveStatusId
                                    WHERE (@SearchValue is null OR  (ILR.Name like @SearchValue OR ILR.Code like @SearchValue)) AND ILR.[ActiveStatusID] <> 3";

                var query = $@"
                              SELECT DISTINCT
                                     ILR.[ID]
                                    ,ILR.[Name]
                                    ,ILR.[Code]
                                    ,ILR.[ActiveStatusID]
                                    ,A.[Name] as ActiveStatus
                                    ,ilr.[CreatedAt]
                                    FROM [ISSUEMANAGEMENT].[IssueLinkReason] ILR
                                    INNER JOIN [Basic].[ActiveStatus] A on A.ID = ILR.ActiveStatusId
                                    WHERE (@SearchValue is null OR  (ILR.Name like @SearchValue OR ILR.Code like @SearchValue)) AND ILR.[ActiveStatusID] <> 3
                                    order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetIssueLinkReasonQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }
}
