using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Read;
using System.Data.SqlClient;


namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueLinkReasons
{
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

        public async Task<Result<List<GetIssueLinkReasonQueryResult>>> GetAll(BaseRequest? baseRequest = null)
        {
            var response = new List<GetIssueLinkReasonQueryResult>();
            int totalCount = 0;
            if (baseRequest != null && !string.IsNullOrEmpty(baseRequest.SearchValue))
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = $@"
                      SELECT DISTINCT ILR.[ID]
                          ,ILR.[Name]
                          ,ILR.[Code]
                          ,ILR.[ActiveStatusID]
                          ,A.[Name] as ActiveStatus
,ilr.[CreatedAt]
                      FROM [ISSUEMANAGEMENT].[IssueLinkReason] ILR
                       INNER JOIN [Basic].[ActiveStatus] A on A.ID = ILR.ActiveStatusId
              WHERE (Name like N'%@SearchValue%' OR Code like '%@SearchValue%') AND [ActiveStatusID] = 1
Order By ilr.[CreatedAt] desc  
";
                    var result = await connection.QueryAsync<GetIssueLinkReasonQueryResult>(query, new { baseRequest.SearchValue });
                    totalCount = result.Count();
                    response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take).Take(baseRequest.Take).ToList();
                }
            }
            else if (baseRequest != null && string.IsNullOrEmpty(baseRequest.SearchValue))
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"
                SELECT DISTINCT ILR.[ID]
                          ,ILR.[Name]
                          ,ILR.[Code]
                          ,ILR.[ActiveStatusID]
                          ,A.[Name] as ActiveStatus
,ilr.[CreatedAt]
                      FROM [ISSUEMANAGEMENT].[IssueLinkReason] ILR
                      INNER JOIN [Basic].[ActiveStatus] A on A.ID = ILR.ActiveStatusId
                      WHERE ILR.[ActiveStatusID] != 3
Order By ilr.[CreatedAt] desc  ";
                    var result = await connection.QueryAsync<GetIssueLinkReasonQueryResult>(query);
                    totalCount = result.Count();
                    response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take).Take(baseRequest.Take).ToList();
                }
            }
            return Result.Ok(response, totalCount); ;
        }
    }
}
