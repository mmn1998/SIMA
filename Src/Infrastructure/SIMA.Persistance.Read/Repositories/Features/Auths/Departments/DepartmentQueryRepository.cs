using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Departments;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Resources;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Departments;

public class DepartmentQueryRepository : IDepartmentQueryRepository
{
    private readonly string _connectionString;

    public DepartmentQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetDepartmentQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT D.[ID] as Id
                  ,D.[Name]
                  ,D.[Code]
	              , (select DP.Name from [Organization].Department DP where  DP.ID = D.ParentID   ) as ParentName
                  , (select DP.Id from [Organization].Department DP where  DP.ID = D.ParentID   ) as ParentId
	              ,C.Name as CompanyName
                  ,C.Id CompanyId
                  ,a.ID ActiveStatusId
                 ,a.Name ActiveStatus
              FROM [Organization].Department D
              join Basic.ActiveStatus a
              on D.ActiveStatusId = a.ID
              LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID AND C.[ActiveStatusID] <> 3
                WHERE  D.Id = @Id

";
            var result = await connection.QueryFirstOrDefaultAsync<GetDepartmentQueryResult>(query, new { Id = id });
            if (result is null) throw new SimaResultException(CodeMessges._100053Code, Messages.DepartmentNotFoundError);
            if (result.ActiveStatusId == 3) throw new SimaResultException(CodeMessges._100033Code, Messages.DepartmentDeleteError);
            if (result.ActiveStatusId == 2 || result.ActiveStatusId == 4) throw new SimaResultException(CodeMessges._100033Code, Messages.DepartmentDeleteError);
            return result;
        }
    }

    public async Task<Result<IEnumerable<GetDepartmentQueryResult>>> GetAll(GetAllDepartmentsQuery? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
SELECT DISTINCT 
D.[ID] as Id
,D.[Name]
,D.[Code]
, (select DP.Name from [Organization].Department DP where  DP.ID = D.ParentID   ) as ParentName
, (select DP.Id from [Organization].Department DP where  DP.ID = D.ParentID   ) as ParentId
,C.Name as CompanyName
,C.Id CompanyId
,a.ID ActiveStatusId
,a.Name ActiveStatus
,d.[CreatedAt]
FROM [Organization].Department D
join Basic.ActiveStatus a on D.ActiveStatusId = a.ID
LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID AND C.[ActiveStatusID] <> 3
WHERE D.[ActiveStatusID] <> 3 AND (@CompanyId is null OR D.Id = @CompanyId
";
            var queryCount = $@" WITH Query as({mainQuery})
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
            string query = $@" WITH Query as({mainQuery})
)
								                SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("CompanyId", request.CompanyId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetDepartmentQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<Result<IEnumerable<GetDepartmentQueryResult>>> GetByCpmpamyId(long companyId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                    SELECT DISTINCT 
                         D.[ID] as Id
                         ,D.[Name]
                     FROM [Organization].Department D
                     WHERE D.ActiveStatusId <> 3 and D.CompanyId = @CompanyId";
            var result = await connection.QueryAsync<GetDepartmentQueryResult>(query, new { CompanyId = companyId });
            return Result.Ok(result);
        }
    }
}
