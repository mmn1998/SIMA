using System.Data.SqlClient;
using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseTypes;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.OperationalStatuses;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.OperationalStatuses;

public class OperationalStatusQueryRepository: IOperationalStatusQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;

    public OperationalStatusQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
              Select
                 l.[Id]
                ,l.[Name]
                ,l.[Code]
                ,l.CreatedAt
                ,l.[ActiveStatusId]
                ,A.[Name] ActiveStatus
            From [Asset].[OperationalStatus] l
            join Basic.ActiveStatus A on l.ActiveStatusId = A.Id
            WHERE l.ActiveStatusId <> 3
            ";
    }
    public async Task<GetOperationalStatusQueryResult> GetById(GetOperationalStatusQuery request)
    { 
        var query = $@"
         {_mainQuery} AND l.[Id] = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetOperationalStatusQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }

    public async Task<Result<IEnumerable<GetOperationalStatusQueryResult>>> GetAll(GetAllOperationalStatusQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
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
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetOperationalStatusQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }
}