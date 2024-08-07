using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Resources;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Positions;

public class PositionQueryRepository : IPositionQueryRepository
{
    private readonly string _connectionString;

    public PositionQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<GetPositionQueryResult> FindById(long Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT P.[ID] as Id
                  ,P.[Name]
                  ,P.[Code] 
                  ,P.[ActiveStatusID]
                  ,A.[Name] as ActiveStatus 
	              ,D.Name as DepartmentName
	              ,C.Name as CompanyName
              FROM [Organization].Position P
              LEFT JOIN [Organization].Department D on D.ID = P.DepartmentID
              LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID
              join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
              WHERE P.[ActiveStatusID] <> 3 AND D.[ActiveStatusID] <> 3 AND C.[ActiveStatusID] <> 3 AND P.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetPositionQueryResult>(query, new { Id });
            if (result is null) throw new SimaResultException(CodeMessges._100054Code, Messages.PositionNotFoundError);
            if (result.IsDeleted) throw new SimaResultException(CodeMessges._100035Code, Messages.PositionDeleteError);
            if (result.IsDeactivated) throw new SimaResultException(CodeMessges._100035Code, Messages.PositionDeleteError);
            return result;
        }
    }
    public async Task<Result<IEnumerable<GetPositionQueryResult>>> GetAll(GetAllPositionsQuery? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @" WITH Query as(
						  SELECT DISTINCT P.[ID] as Id
    ,P.[Name]
    ,P.[Code]
    ,P.[ActiveStatusID]
    ,A.[Name] as ActiveStatus 
    ,D.Name as DepartmentName
    ,C.Name as CompanyName
    ,p.[CreatedAt]
FROM [Organization].Position P
LEFT JOIN [Organization].Department D on D.ID = P.DepartmentID
LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID
join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
WHERE P.[ActiveStatusID] <> 3 AND D.[ActiveStatusID] <> 3 AND C.[ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
            string query = $@"WITH Query as(
							SELECT DISTINCT P.[ID] as Id
    ,P.[Name]
    ,P.[Code]
    ,P.[ActiveStatusID]
    ,A.[Name] as ActiveStatus 
    ,D.Name as DepartmentName
    ,C.Name as CompanyName
    ,p.[CreatedAt]
FROM [Organization].Position P
LEFT JOIN [Organization].Department D on D.ID = P.DepartmentID
LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID
join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
WHERE P.[ActiveStatusID] <> 3 AND D.[ActiveStatusID] <> 3 AND C.[ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);

            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetPositionQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }
}
