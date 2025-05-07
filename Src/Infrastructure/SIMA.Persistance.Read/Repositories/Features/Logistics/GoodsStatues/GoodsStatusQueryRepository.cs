using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Logistics.GoodsStatuses;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.GoodsStatues;

public class GoodsStatusQueryRepository : IGoodsStatusQueryRepository
{
    private readonly string _connectionString;
    public GoodsStatusQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetAllGoodsStatusQueryResult>>> GetAll(GetAllGoodsStatusQuery request)
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
								,A.[Name] ActiveStatus
                                ,F.IsRequiredItConfirmation
                                ,F.IsFinal
								From Logistics.GoodsStatus F
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
                var response = await multi.ReadAsync<GetAllGoodsStatusQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetAllGoodsStatusQueryResult> GetById(long id)
    {
        var query = @"
Select 
F.[Id]
,F.[Name]
,F.[Code]
,F.CreatedAt
,F.[ActiveStatusId]
,A.[Name] ActiveStatus
,F.IsRequiredItConfirmation
,F.IsFinal
From Logistics.GoodsStatus F
join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
WHERE F.[ActiveStatusID] <> 3 and F.Id = @id
";
        using (var connection = new SqlConnection(_connectionString))
        {
            var response = await connection.QueryFirstOrDefaultAsync<GetAllGoodsStatusQueryResult>(query, new { id }) ?? throw SimaResultException.NotFound;
            return response;
        }
    }

    public async Task<long> GetStatusByCode(string code)
    {
        {
            var query = @"
                        Select
                        Id
                        from Logistics.GoodsStatus S
                        where S.Code = @code and S.ActiveStatusId <>3.Id
                        WHERE F.[ActiveStatusID] <> 3 and F.Id = @id
";
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = await connection.QueryFirstOrDefaultAsync<long>(query, new { code });
                if (response == 0 || response == null)
                    throw SimaResultException.NotFound;
                return response;
            }
        }
    }
}
