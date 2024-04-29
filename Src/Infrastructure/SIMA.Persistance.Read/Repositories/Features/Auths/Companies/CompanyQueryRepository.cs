using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Companies;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Companies;

public class CompanyQueryRepository : ICompanyQueryRepository
{
    private readonly string _connectionString;

    public CompanyQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetCompanyQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT C.[ID] as Id
                  ,C.[Name]
                  ,C.[Code]
	              , (select PC.name from[Organization].[Company] PC where  PC.ID = C.ParentID and PC.ActiveStatusID <> 3 ) ParentName
                   ,a.ID ActiveStatusId
                   ,a.Name ActiveStatus
                     FROM [Organization].[Company] C
                          join Basic.ActiveStatus a
                         on c.ActiveStatusId = a.ID

              WHERE C.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetCompanyQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.CompanyNotFoundError;
            if (result.ActiveStatusId == 3) throw SimaResultException.ComapnyDeleteError;
            if (result.ActiveStatusId == 2 || result.ActiveStatusId == 4) throw SimaResultException.ComapnyDeleteError;
            return result;
        }
    }

    public async Task<Result<IEnumerable<GetCompanyQueryResult>>> GetAll(GetAllCompanyQuery? request = null)
    {
        

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var queryCount = @" SELECT  COUNT(*) Result
                                 FROM [Organization].[Company] C
                                 join Basic.ActiveStatus a
                                 on c.ActiveStatusId = a.ID
                                  WHERE (@SearchValue is null OR (C.[Name] like @SearchValue OR C.[Code] like @SearchValue)) AND C.[ActiveStatusID] <> 3";
            string query = $@"
                                 SELECT DISTINCT 
                                        C.[ID] as Id
                                       ,C.[Name]
                                       ,C.[Code]
	                                   , (select PC.name from[Organization].[Company] PC where  PC.ID = C.ParentID and PC.ActiveStatusID = 1 )ParentName
                                      ,a.ID ActiveStatusId
                                      ,a.Name ActiveStatus
                                      ,C.[CreatedAt]
                                      FROM [Organization].[Company] C
                                           join Basic.ActiveStatus a on c.ActiveStatusId = a.ID
                                    WHERE (@SearchValue is null OR (C.[Name] like @SearchValue OR C.[Code] like @SearchValue)) AND C.[ActiveStatusID] <> 3
                                  order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                                   OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                request.Skip,
                request.PageSize
            }))
            {
                var response = await multi.ReadAsync<GetCompanyQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
    }
}
