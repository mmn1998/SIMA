using System.Data.SqlClient;
using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.MainAggregates;
using SIMA.Application.Query.Contract.Features.Auths.NetworkProtocols;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Interfaces;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.NetworkProtocols;

public class NetworkProtocolReadRepsoitory : INetworkProtocolReadRepository 
{
    private readonly string _connectionString;
    public NetworkProtocolReadRepsoitory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    
    public async Task<Result<IEnumerable<GetAllNetworkProtocolQueryResult>>> GetAll(GetAllNetworlProtocolQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();


            var mainQuery = @"
select 
N.Id,
N.Name,
N.Code,
N.CreatedAt,
N.ActiveStatusId
from Basic.NetworkProtocol N 
where N.ActiveStatusId <> 3
       ";
            string queryCount = $@"WITH Query as(
						{mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";
            string query = $@"WITH Query as(
							{mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetAllNetworkProtocolQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }
}