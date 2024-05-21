using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingStatus;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.SubjectPriorities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.SubjectPriorities;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.MeetingHoldingStatus
{
    public class MeetingHoldingStatusQueryRepository : IMeetingHoldingStatusQueryRepository
    {
        private readonly string _connectionString;
        public MeetingHoldingStatusQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }

        public async Task<Result<List<GetMeetingHoldingStatusQueryResult>>> GetAll(GetAllMeetingHoldingStatusQuery request)
        {
            var response = new List<GetMeetingHoldingStatusQueryResult>();
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
                        SELECT M.[Id]
                                  ,M.[Name]
                                  ,M.[Code]
	                              ,S.[Name] ActiveStatus
	                              ,S.[Id] ActiveStatusId
                        FROM [SecurityCommitee].[MeetingHoldingStatus] M
                        INNER JOIN [Basic].[ActiveStatus] S on S.ID = M.ActiveStatusId
                        WHERE M.ActiveStatusId <> 3
                    ) as Query
                    WHERE {filterClause};";
                    string query = $@"
                    SELECT *
                    FROM (
                        SELECT M.[Id]
                                  ,M.[Name]
                                  ,M.[Code]
	                              ,S.[Name] ActiveStatus
	                              ,S.[Id] ActiveStatusId
                        FROM [SecurityCommitee].[MeetingHoldingStatus] M
                        INNER JOIN [Basic].[ActiveStatus] S on S.ID = M.ActiveStatusId
                        WHERE M.ActiveStatusId <> 3
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
                        response = (await multi.ReadAsync<GetMeetingHoldingStatusQueryResult>()).ToList();
                        var count = await multi.ReadSingleAsync<int>();
                        return Result.Ok(response, count, request.PageSize, request.Page);
                    }
                }
                else
                {
                    string queryCount = @"
                              SELECT COUNT(*) Result
                                      FROM [SecurityCommitee].[MeetingHoldingStatus] M
                                      INNER JOIN [Basic].[ActiveStatus] S on S.ID = M.ActiveStatusId
                                       WHERE (@SearchValue is null OR  (M.[Name] like @SearchValue or M.[Code] like @SearchValue or S.[Name] like @SearchValue)) and 
                                       M.ActiveStatusId <> 3 ";

                    string query = $@"
                              SELECT M.[Id]
                                  ,M.[Name]
                                  ,M.[Code]
	                              ,S.[Name] ActiveStatus
	                              ,S.[Id] ActiveStatusId
                              FROM [SecurityCommitee].[MeetingHoldingStatus] M
                              INNER JOIN [Basic].[ActiveStatus] S on S.ID = M.ActiveStatusId
                              WHERE (@SearchValue is null OR  (M.[Name] like @SearchValue or M.[Code] like @SearchValue or S.[Name] like @SearchValue)) and 
                                    M.ActiveStatusId <> 3  
                                 order by {request.Sort?.Replace(":", " ") ?? "M.CreatedAt desc"}            
                                  OFFSET @Skip rows FETCH NEXT @PageSize rows only ;";
                    using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                    {
                        SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                        request.Skip,
                        request.PageSize
                    }))
                    {
                        response = (await multi.ReadAsync<GetMeetingHoldingStatusQueryResult>()).ToList();
                        var count = await multi.ReadSingleAsync<int>();
                        return Result.Ok(response, count, request.PageSize, request.Page);
                    }
                }
            }
        }

        public async Task<GetMeetingHoldingStatusQueryResult> GetById(long Id)
        {
            try
            {
                var response = new GetMeetingHoldingStatusQueryResult();
                string query = $@"
                    SELECT M.[Id]
                          ,M.[Name]
                          ,M.[Code]
                    	  ,A.[Name] ActiveStatus
                          ,A.[Id] ActiveStatusId
                      FROM [SecurityCommitee].[MeetingHoldingStatus] M
                      INNER JOIN [Basic].[ActiveStatus] A ON A.ID = M.ActiveStatusId
                      WHERE M.[Id] = @Id
                    ";
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryFirstOrDefaultAsync<GetMeetingHoldingStatusQueryResult>(query, new { Id = Id });
                    result.NullCheck();
                    response = result ?? throw SimaResultException.NotFound;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
