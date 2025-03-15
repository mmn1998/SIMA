using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedureTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.DataProcedureTypes;

public class DataProcedureTypeQueryRepository : IDataProcedureTypeQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public DataProcedureTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
Select
     DPT.[Id]
    ,DPT.[Name]
    ,DPT.[Code]
    ,DPT.CreatedAt
    ,DPT.[ActiveStatusId]
    ,A.[Name] ActiveStatus
From AssetAndConfiguration.DataProcedureType DPT
join Basic.ActiveStatus A on DPT.ActiveStatusId = A.Id
WHERE DPT.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetDataProcedureTypeQueryResult>>> GetAll(GetAllDataProcedureTypesQuery request)
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
        var response = await multi.ReadAsync<GetDataProcedureTypeQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetDataProcedureTypeQueryResult> GetById(GetDataProcedureTypeQuery request)
    {
        var query = $@"
         {_mainQuery} AND DPT.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetDataProcedureTypeQueryResult>(query, new { request.Id });
        return result ?? throw SimaResultException.NotFound;
    }
}