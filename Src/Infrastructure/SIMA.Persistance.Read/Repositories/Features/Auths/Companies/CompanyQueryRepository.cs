using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Companies;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Resources;

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
                  ,C.[ParentId]
	              , (select PC.name from[Organization].[Company] PC where  PC.ID = C.ParentID and PC.ActiveStatusID <> 3 ) ParentName
                   ,a.ID ActiveStatusId
                   ,a.Name ActiveStatus
                     FROM [Organization].[Company] C
                          join Basic.ActiveStatus a
                         on c.ActiveStatusId = a.ID
              WHERE C.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetCompanyQueryResult>(query, new { Id = id });
            if (result is null) throw new SimaResultException(CodeMessges._100052Code, Messages.CompanyNotFoundError);
            if (result.ActiveStatusId == 3) throw new SimaResultException(CodeMessges._100031Code, Messages.ComapnyDeleteError);
            if (result.ActiveStatusId == 2 || result.ActiveStatusId == 4) throw new SimaResultException(CodeMessges._100031Code, Messages.ComapnyDeleteError);
            return result;
        }
    }

    public async Task<Result<IEnumerable<GetCompanyQueryResult>>> GetAll(GetAllCompanyQuery? request = null)
    {


        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var queryCount = @" WITH Query as(
                                 SELECT DISTINCT 
                                        C.[ID] as Id
                                       ,C.[Name]
                                       ,C.[Code]
                                       ,C.[ParentId]
	                                   ,(select PC.name from[Organization].[Company] PC where  PC.ID = C.ParentID and PC.ActiveStatusID = 1 ) ParentName
                                      ,a.ID ActiveStatusId
                                      ,a.Name ActiveStatus
                                      ,C.[CreatedAt]
                                      FROM [Organization].[Company] C
                                      join Basic.ActiveStatus a on c.ActiveStatusId = a.ID
                                      WHERE C.[ActiveStatusID] <> 3)
								SELECT COUNT(*) Result FROM Query
                                /**where**/
                                ; ";
            string query = $@"WITH Query as(
                                 SELECT DISTINCT 
                                        C.[ID] as Id
                                       ,C.[Name]
                                       ,C.[Code]
                                       ,C.[ParentId]
	                                   , (select PC.name from[Organization].[Company] PC where  PC.ID = C.ParentID and PC.ActiveStatusID = 1 ) ParentName
                                      ,a.ID ActiveStatusId
                                      ,a.Name ActiveStatus
                                      ,C.[CreatedAt]
                                      FROM [Organization].[Company] C
                                      join Basic.ActiveStatus a on c.ActiveStatusId = a.ID
                                      WHERE C.[ActiveStatusID] <> 3)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetCompanyQueryResult>();
                return Result.Ok(response,request , count);
            }
        }
    }
}
