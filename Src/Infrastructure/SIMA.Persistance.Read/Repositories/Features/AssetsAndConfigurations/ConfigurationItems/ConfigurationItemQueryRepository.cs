using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItems;

public class ConfigurationItemQueryRepository : IConfigurationItemQueryRepository
{
    private readonly string _connectionString;
    public ConfigurationItemQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }


    public async Task<Result<IEnumerable<GetConfigurationItemQueryResult>>> GetAll(GetAllConfigurationItemsQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                              Select 
	                             CI.[Id]
	                            ,CI.Description
                                ,CI.CreatedAt
	                            ,A.[Name] ActiveStatus
	                            From AssetAndConfiguration.ConfigurationItem CI
	                            join Basic.ActiveStatus A on CI.ActiveStatusId = A.Id
	                            WHERE CI.[ActiveStatusID] <> 3
							";
            var queryCount = $@"
                             WITH Query as(	{mainQuery})
								SELECT Count(*) FROM Query
								 /**where**/
								 ;";

            string query = $@"WITH Query as({mainQuery})
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = (queryCount + query).GenerateQuery(request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetConfigurationItemQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public Task<Result<GetConfigurationItemQueryResult>> GetById(long id)
    {
        throw new NotImplementedException();
    }
}
