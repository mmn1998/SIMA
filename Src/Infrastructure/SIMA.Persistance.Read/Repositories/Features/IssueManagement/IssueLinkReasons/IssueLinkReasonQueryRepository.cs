using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentTypes;
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
           
                var queryCount = @"   WITH Query as(
						  SELECT DISTINCT
       ILR.[ID]
      ,ILR.[Name]
      ,ILR.[Code]
      ,ILR.[ActiveStatusID]
      ,A.[Name] as ActiveStatus
      ,ilr.[CreatedAt]
      FROM [ISSUEMANAGEMENT].[IssueLinkReason] ILR
      INNER JOIN [Basic].[ActiveStatus] A on A.ID = ILR.ActiveStatusId
      WHERE  ILR.[ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

                var query = $@" WITH Query as(
							SELECT DISTINCT
       ILR.[ID]
      ,ILR.[Name]
      ,ILR.[Code]
      ,ILR.[ActiveStatusID]
      ,A.[Name] as ActiveStatus
      ,ilr.[CreatedAt]
      FROM [ISSUEMANAGEMENT].[IssueLinkReason] ILR
      INNER JOIN [Basic].[ActiveStatus] A on A.ID = ILR.ActiveStatusId
      WHERE  ILR.[ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetIssueLinkReasonQueryResult>();
                return Result.Ok(response, request, count);
            }            
        }
    }
}
