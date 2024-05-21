using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingReasons;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.MeetingHoldingReasons;

public class MeetingHoldingReasonQueryRepository : IMeetingHoldingReasonQueryRepository
{
    private readonly string _connectionString;
    public MeetingHoldingReasonQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<List<GetMeetingHoldingReasonQueryResult>>> GetAll(GetAllMeetingHoldingReasonsQuery request)
    {
        var response = new List<GetMeetingHoldingReasonQueryResult>();
        int totalCount = 0;

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
                        SELECT  MHR.[Id]
                              ,MHR.[Code]
                              ,MHR.[Name]
                              ,S.[Name] ActiveStatus
                              ,MHR.[CreatedAt]
                          FROM [SecurityCommitee].[MeetingHoldingReason] MHR
                          INNER JOIN [Basic].[ActiveStatus] S ON S.ID = MHR.ActiveStatusId
                        WHERE MHR.ActiveStatusId <> 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT  MHR.[Id]
                              ,MHR.[Code]
                              ,MHR.[Name]
                              ,S.[Name] ActiveStatus
                              ,MHR.[CreatedAt]
                          FROM [SecurityCommitee].[MeetingHoldingReason] MHR
                          INNER JOIN [Basic].[ActiveStatus] S ON S.ID = MHR.ActiveStatusId
                        WHERE MHR.ActiveStatusId <> 3
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
                    response = (await multi.ReadAsync<GetMeetingHoldingReasonQueryResult>()).ToList();
                    totalCount = await multi.ReadSingleAsync<int>();
                }
            }
            else
            {
                string queryCount = $@"
                                    SELECT COUNT(*) Result
                                      FROM [SecurityCommitee].[MeetingHoldingReason] MHR
                                        INNER JOIN [Basic].[ActiveStatus] S ON S.ID = MHR.ActiveStatusId
                                       WHERE (@SearchValue is null OR  (MHR.[Name] like @SearchValue or MHR.[Code] like @SearchValue or S.[Name] like @SearchValue)) and 
                                       MHR.ActiveStatusId <> 3 ;";
                string query = $@"
                            SELECT  MHR.[Id]
                              ,MHR.[Code]
                              ,MHR.[Name]
                              ,S.[Name] ActiveStatus
                              ,MHR.[CreatedAt]
                          FROM [SecurityCommitee].[MeetingHoldingReason] MHR
                          INNER JOIN [Basic].[ActiveStatus] S ON S.ID = MHR.ActiveStatusId
                                WHERE (@SearchValue is null OR  (SP.[Ordering] like @SearchValue or SP.[Name] like @SearchValue or SP.[Code] like @SearchValue or S.[Name] like @SearchValue)) and 
                                           MHR.ActiveStatusId <> 3  
                                 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}            
                                  OFFSET @Skip rows FETCH NEXT @Take rows only ;
";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    response = (await multi.ReadAsync<GetMeetingHoldingReasonQueryResult>()).ToList();
                    totalCount = await multi.ReadSingleAsync<int>();
                }
            }
        }
        return Result.Ok(response, totalCount, request.PageSize, request.Page);
    }

    public async Task<GetMeetingHoldingReasonQueryResult> GetById(long Id)
    {
        var response = new GetMeetingHoldingReasonQueryResult();

        string query = @"
SELECT  MHR.[Id]
      ,MHR.[Code]
      ,MHR.[Name]
  FROM [SecurityCommitee].[MeetingHoldingReason] MHR
  INNER JOIN [Basic].[ActiveStatus] S ON S.ID = MHR.ActiveStatusId
  WHERE MHR.Id = @Id
";
        using (var connection = new SqlConnection(_connectionString))
        {
            var result = await connection.QueryFirstOrDefaultAsync<GetMeetingHoldingReasonQueryResult>(query, new { Id = Id });
            result.NullCheck();
            response = result ?? throw SimaResultException.NotFound;
        }
        return response;
    }
}
