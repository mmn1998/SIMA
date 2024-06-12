using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ChannelTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ChannelTypes;

public class ChannelTypeQueryRepository : IChannelTypeQueryRepository
{
    private readonly string _connectionString;
    public ChannelTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetChannelTypeQueryResult>>> GetAll(GetAllChannelTypeQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                               Select 
								 F.[Id]
								,F.[Name]
								,F.[Code]
                                ,F.CreatedAt
								,F.[ActiveStatusId]
								,A.[Name] ActiveStatusName
								From ServiceCatalog.ChannelType F
								join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
								WHERE F.[ActiveStatusID] <> 3
							";
            var queryCount = $@"
                             WITH Query as(	{mainQuery}	)
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
                var response = await multi.ReadAsync<GetChannelTypeQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetChannelTypeQueryResult> GetById(GetChannelTypeQuery request)
    {
        var query = @"
          SELECT C.[Id]
              ,C.[Name]
              ,C.[Code]
              ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ChannelType] C
          INNER JOIN [Basic].[ActiveStatus] A ON C.ActiveStatusId = A.ID
          WHERE C.[Id] = @Id AND C.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetChannelTypeQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }
}