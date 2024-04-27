using Dapper;
using Microsoft.Extensions.Configuration;
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
        string query = @$"SELECT Value,Lable FROM [Basic].[{viewName}]";
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
                              SELECT  COUNT(*) Result
                               From Authentication.Form F
                               join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
                               WHERE (@SearchValue is null OR(F.Name like @SearchValue OR F.Code like @SearchValue)) AND F.[ActiveStatusID] <> 3";

            string query = $@"
                                 Select 
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
                            WHERE (@SearchValue is null OR(F.Name like @SearchValue OR F.Code like @SearchValue)) AND F.[ActiveStatusID] <> 3
                             order by {request.Sort?.Replace(":", " ") ?? "F.CreatedAt desc"}
                              OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = "%" + request.Filter + "%",
                request.Skip,
                request.PageSize
            }))
            {
                var response = await multi.ReadAsync<GetFormQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
    }

}

