using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Departments;
using SIMA.Application.Query.Contract.Features.Auths.Forms;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Forms;

public class FormQueryRepository : IFormQueryRepository
{
    private readonly string _connectionString;
    public FormQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<List<GetViewResult>> FetchFromView(string viewName)
    {
        string query = @$"SELECT Value,Lable FROM [{viewName}]";
        using (var connection = new SqlConnection(_connectionString))
        {
            var result = (await connection.QueryAsync<GetViewResult>(query)).ToList();
            return result;
        }
    }

    public async Task<GetFormQueryResult> FindById(long id)
    {
        var query = @"Select 
                             F.[Id]
                            ,F.[Name]
                            ,F.[Title]
                            ,F.[Code]
                            ,F.[IsSystemForm]
                            ,F.[JsonContent]
                            ,F.[ActiveStatusId]
                            ,A.[Name] ActiveStatusName
                            From Authentication.Form F
                            join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
                            Where F.ActiveStatusId <> 3 and F.Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryFirstOrDefaultAsync<GetFormQueryResult>(query, new { Id = id });
        result.NullCheck();
        return result;
    }

    public async Task<Result<IEnumerable<GetFormQueryResult>>> GetAll(GetAllFormQuery? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var queryCount = @"
                             WITH Query as(
						       Select 
								 F.[Id]
								,F.[Name]
								,F.[Title]
								,F.[Code]
                                ,F.CreatedAt
								,F.[IsSystemForm]
								,F.[JsonContent]
								,F.[ActiveStatusId]
								,A.[Name] ActiveStatusName
								From Authentication.Form F
								join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
								WHERE F.[ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";

            string query = $@"WITH Query as(
							     Select 
								 F.[Id]
								,F.[Name]
								,F.[Title]
								,F.[Code]
								,F.[IsSystemForm]
                                ,F.CreatedAt
								,F.[JsonContent]
								,F.[ActiveStatusId]
								,A.[Name] ActiveStatusName
								From Authentication.Form F
								join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
								WHERE   F.[ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetFormQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<Result<IEnumerable<GetFormFieldsQueryResult>>> GetAllFormFields(GetAllFormFieldsQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var queryCount = @" WITH Query as(
						  SELECT FF.[Id]
     ,FF.[FormId]
     ,FF.[Name]
     ,FF.[Code]
     ,FF.[Type]
 ,F.CreatedAt
     ,FF.[ActiveStatusId]
     ,F.[Name] FormName
     ,A.[Name] ActiveStatus
 FROM [Authentication].[FormField] FF
 INNER JOIN [Authentication].[Form] F ON F.Id = FF.FormId AND F.ActiveStatusId <> 3
 INNER JOIN [Basic].[ActiveStatus] A ON A.ID = FF.ActiveStatusId
 WHERE FF.ActiveStatusId <> 3 AND (@FormId is null or F.Id = @FormId)
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";

            string query = $@"WITH Query as(
												  SELECT FF.[Id]
    ,FF.[FormId]
    ,FF.[Name]
    ,FF.[Code]
    ,FF.[Type]
 ,F.CreatedAt
    ,FF.[ActiveStatusId]
    ,F.[Name] FormName
    ,A.[Name] ActiveStatus
FROM [Authentication].[FormField] FF
INNER JOIN [Authentication].[Form] F ON F.Id = FF.FormId AND F.ActiveStatusId <> 3
INNER JOIN [Basic].[ActiveStatus] A ON A.ID = FF.ActiveStatusId
WHERE FF.ActiveStatusId <> 3 AND (@FormId is null or F.Id = @FormId)
							
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);

            dynaimcParameters.Item2.Add("FormId", request.FormId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetFormFieldsQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }
}

