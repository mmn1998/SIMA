using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Back_UpMethods;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Back_UpMethods;

public class BackupMethodQueryRepository : IBackupMethodQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public BackupMethodQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT
     BM.[Id]
    ,BM.[Name]
    ,BM.[Code]
    ,BM.CreatedAt
    ,BM.[ActiveStatusId]
    ,A.[Name] ActiveStatus
From Asset.BackupMethod BM
JOIN Basic.ActiveStatus A on BM.ActiveStatusId = A.Id
WHERE BM.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetBackupMethodQueryResult>>> GetAll(GetAllBackupMethodsQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var queryCount = $@"
                             WITH Query as(	{_mainQuery}	)
								SELECT Count(*) FROM Query
								 /**where**/
								 ;";

        string query = $@"WITH Query as(
							 {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);

        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetBackupMethodQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetBackupMethodQueryResult> GetById(GetBackupMethodQuery request)
    {
        var query = $@"
         {_mainQuery} AND BM.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetBackupMethodQueryResult>(query, new { request.Id });
        return result ?? throw SimaResultException.NotFound;
    }
}