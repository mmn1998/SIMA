using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Departments;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

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
	              ,C.Name as CompanyName
                  ,a.ID ActiveStatusId
                 ,a.Name ActiveStatus
              FROM [Organization].Department D
              join Basic.ActiveStatus a
              on D.ActiveStatusId = a.ID
              LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID AND C.[ActiveStatusID] <> 3
                WHERE  D.Id = @Id

";
            var result = await connection.QueryFirstOrDefaultAsync<GetDepartmentQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.DepartmentNotFoundError;
            if (result.ActiveStatusId == 3) throw SimaResultException.DepartmentDeleteError;
            if (result.ActiveStatusId == 2 || result.ActiveStatusId == 4) throw SimaResultException.DepartmentDeleteError;
            return result;
        }
    }

    public async Task<Result<IEnumerable<GetDepartmentQueryResult>>> GetAll(GetAllDepartmentsQuery? request = null)
    {
        
                    using (var connection = new SqlConnection(_connectionString))
            {
                            await connection.OpenAsync();
            var queryCount = @" SELECT  COUNT(*) Result
                                 FROM [Organization].Department D
                                       join Basic.ActiveStatus a
                                      on D.ActiveStatusId = a.ID
                                      LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID AND C.[ActiveStatusID] <> 3
                                        WHERE (@SearchValue is null OR(D.[Name] like @SearchValue OR C.[Name] like @SearchValue OR D.[Code] like @SearchValue)) AND D.[ActiveStatusID] <> 3 ";
                string query = $@"
                                  SELECT DISTINCT 
                                         D.[ID] as Id
                                        ,D.[Name]
                                        ,D.[Code]
	                                    , (select DP.Name from [Organization].Department DP where  DP.ID = D.ParentID   ) as ParentName
	                                    ,C.Name as CompanyName
                                        ,a.ID ActiveStatusId
                                        ,a.Name ActiveStatus
                                        ,d.[CreatedAt]
                                        FROM [Organization].Department D
                                        join Basic.ActiveStatus a on D.ActiveStatusId = a.ID
                                        LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID AND C.[ActiveStatusID] <> 3
                                        WHERE (@SearchValue is null OR (D.[Name] like @SearchValue OR C.[Name] like @SearchValue OR D.[Code] like @SearchValue)) AND D.[ActiveStatusID] <> 3
                                        order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                                        OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = "%" + request.Filter + "%",
                request.Skip,
                request.PageSize
            }))
            {
                var response = await multi.ReadAsync<GetDepartmentQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
    }
}
