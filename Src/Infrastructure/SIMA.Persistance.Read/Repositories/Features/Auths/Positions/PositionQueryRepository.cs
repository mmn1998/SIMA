using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

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
            if (result is null) throw SimaResultException.PositionNotFoundError;
            if (result.IsDeleted) throw SimaResultException.PositionDeleteError;
            if (result.IsDeactivated) throw SimaResultException.PositionDeleteError;
            return result;
        }
    }


    public async Task<Result<IEnumerable<GetPositionQueryResult>>> GetAll(GetAllPositionsQuery? request = null)
    {
                        using (var connection = new SqlConnection(_connectionString))
        {
                        string queryCount = @"
                 SELECT COUNT(*) Result
                 FROM [Organization].Position P
                 LEFT JOIN [Organization].Department D on D.ID = P.DepartmentID
                 LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID
                 join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                 WHERE P.[ActiveStatusID] <> 3 AND D.[ActiveStatusID] <> 3 AND C.[ActiveStatusID] <> 3
                 and (@SearchValue is null OR P.[Name] like @SearchValue or P.[Code] like @SearchValue)";
                await connection.OpenAsync();
            string query = $@"
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
                var response = await multi.ReadAsync<GetPositionQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }

    }
}
