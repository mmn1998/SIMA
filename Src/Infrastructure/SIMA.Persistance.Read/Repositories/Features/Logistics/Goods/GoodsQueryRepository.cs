using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Logistics.Goods;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.Goods;

public class GoodsQueryRepository : IGoodsQueryRepository
{
    private readonly string _connectionString;
    public GoodsQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetGoodsQueryResult>>> GetAll(GetAllGoodsesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                               Select 
								 F.[Id]
								,F.[Name]
								,F.[Code]
								,F.[GoodsCategoryId]
								,F.[UnitMeasurementId]
								,F.[IsFixedAsset]
								,gc.Name GoodsCategory
								,um.Name UnitMeasurement
                                ,F.CreatedAt
								,F.[ActiveStatusId]
								,A.[Name] ActiveStatus
								From Logistics.Goods F
								join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
                                inner join [Logistics].[GoodsCategory] gc on F.GoodsCategoryId = gc.Id
                                inner join [Logistics].[UnitMeasurement] um on F.UnitMeasurementId = um.Id
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
            var dynaimcParameters = (queryCount + query).GenerateQuery(request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetGoodsQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetGoodsQueryResult> GetById(GetGoodsQuery request)
    {
        var query = @"
          Select 
			 F.[Id]
			,F.[Name]
			,F.[Code]
			,F.[GoodsCategoryId]
			,F.[UnitMeasurementId]
			,F.[IsFixedAsset]
			,gc.Name GoodsCategory
			,um.Name UnitMeasurement
            ,F.CreatedAt
			,F.[ActiveStatusId]
			,A.[Name] ActiveStatus
			From Logistics.Goods F
			join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
            inner join [Logistics].[GoodsCategory] gc on F.GoodsCategoryId = gc.Id
            inner join [Logistics].[UnitMeasurement] um on F.UnitMeasurementId = um.Id
          WHERE F.[Id] = @Id AND F.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetGoodsQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }   
}