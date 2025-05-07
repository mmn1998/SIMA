using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.AddressTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.AddressTypes;

public class AddressTypeQueryRepository : IAddressTypeQueryRepository
{
    private readonly string _connectionString;

    public AddressTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetAddressTypeQueryResult> FindById(long id)
    {
        var query = @"SELECT 
                           at.[ID] Id
                          ,at.[Name]
                          ,at.[Code]
                          ,at.[ActiveStatusID]
                          ,at.[CreatedAt]
                          ,at.[CreatedBy]
                          ,at.[ModifiedAt]
                          ,at.[ModifiedBy],
                          a.ID ActiveStatusId, 
                          a.Name ActiveStatus 
                      FROM [Basic].[AddressType] at
                             join Basic.ActiveStatus a
                             on at.ActiveStatusId = a.ID
                      WHERE at.Id = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<GetAddressTypeQueryResult>(query, new { Id = id });
            return result ?? throw SimaResultException.NotFound;
        }


    }

    public async Task<Result<List<GetAddressTypeQueryResult>>> GetAll(GetAllAddressTypesQuery request)
    {


        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var queryCount = @" WITH Query as(
                                 SELECT DISTINCT 
                                    at.[ID] as Id
                                   ,at.[Name]
                                   ,at.[Code]
                                   ,a.ID ActiveStatusId
                                   ,a.Name ActiveStatus
                                   ,at.[CreatedAt]
                               FROM [Basic].[AddressType]  at
                                    join Basic.ActiveStatus a
                                    on at.ActiveStatusId = a.ID
                                WHERE  at.[ActiveStatusID] <> 3)
								SELECT COUNT(*) Result FROM Query
								 /**where**/   ; ";

            string query = $@"WITH Query as(
                                 SELECT DISTINCT 
                                    at.[ID] as Id
                                   ,at.[Name]
                                   ,at.[Code]
                                   ,a.ID ActiveStatusId
                                   ,a.Name ActiveStatus
                                   ,at.[CreatedAt]
                               FROM [Basic].[AddressType]  at
                                    join Basic.ActiveStatus a
                                    on at.ActiveStatusId = a.ID
                                WHERE  at.[ActiveStatusID] <> 3)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynamicParams = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynamicParams.Item1.RawSql, dynamicParams.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = (await multi.ReadAsync<GetAddressTypeQueryResult>()).ToList();
                
                return Result.Ok(response, request, count);
            }
        }
    }
    public async Task<List<GetAddressTypeQueryResult>> GetAllForRedis()
    {

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = $@"
                                 SELECT DISTINCT 
                                    at.[ID] as Id
                                   ,at.[Name]
                                   ,at.[Code]
                                   ,a.ID ActiveStatusId
                                   ,a.Name ActiveStatus
                                   ,at.[CreatedAt]
                               FROM [Basic].[AddressType]  at
                                    join Basic.ActiveStatus a
                                    on at.ActiveStatusId = a.ID
                                WHERE  at.[ActiveStatusID] <> 3 ;";
            var result = (await connection.QueryAsync<GetAddressTypeQueryResult>(query)).ToList();
            return result;
        }
    }




}


