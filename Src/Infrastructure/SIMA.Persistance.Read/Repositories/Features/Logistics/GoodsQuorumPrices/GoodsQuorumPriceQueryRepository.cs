using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Logistics.GoodsQuorumPrices;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.GoodsQuorumPrices;

public class GoodsQuorumPriceQueryRepository : IGoodsQuorumPriceQueryRepository
{
    private readonly string _connectionString;
    public GoodsQuorumPriceQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetGoodsQuorumPriceQueryResult>>> GetAll(GetAllGoodsQuorumPriceQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                               Select 
								 F.[Id]
								,F.[Name]
								,F.[Code]
								,F.[MaxPrice]
								,F.[MinPrice]
								,F.[IsRequiredCeoConfirmation]
								,F.[IsRequiredBoardConfirmation]
								,F.[IsRequiredSupplierWrittenInquiry]
                                ,F.CreatedAt
								,F.[ActiveStatusId]
								,A.[Name] ActiveStatus
								From Logistics.GoodsQuorumPrice F
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
            var dynaimcParameters = (queryCount + query).GenerateQuery(request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetGoodsQuorumPriceQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetGoodsQuorumPriceQueryResult> GetById(GetGoodsQuorumPriceQuery request)
    {
        var query = @"
          SELECT C.[Id]
              ,C.[Name]
              ,C.[Code] 
			    ,C.[MaxPrice]
			    ,C.[MinPrice]
			    ,C.[IsRequiredCeoConfirmation]
			    ,C.[IsRequiredBoardConfirmation]
			    ,C.[IsRequiredSupplierWrittenInquiry]
              ,A.[Name] ActiveStatus
          FROM [Logistics].[GoodsQuorumPrice] C
          INNER JOIN [Basic].[ActiveStatus] A ON C.ActiveStatusId = A.ID
          WHERE C.[Id] = @Id AND C.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetGoodsQuorumPriceQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }
}