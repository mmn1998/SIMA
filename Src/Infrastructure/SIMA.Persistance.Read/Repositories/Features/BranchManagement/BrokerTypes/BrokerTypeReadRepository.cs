using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;
using SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.BrokerTypes;

public class BrokerTypeReadRepository : IBrokerTypeReadRepository
{
    private readonly string _connectionString;
    public BrokerTypeReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetBrokerTypeQueryResult>>> GetAll(GetAllBrokerTypesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
                string queryCount = @" WITH Query as(
						   SELECT DISTINCT BT.[ID]
      ,BT.[Name]
      ,BT.[Code]
      	 ,A.Name ActiveStatus
      	 ,BT.ActiveStatusId 
      ,bt.[CreatedAt]
  FROM [Bank].[BrokerType] BT
  INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID
  WHERE  BT.ActiveStatusId != 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;  ";



                string query = $@" WITH Query as(
							 SELECT DISTINCT BT.[ID]
      ,BT.[Name]
      ,BT.[Code]
      	 ,A.Name ActiveStatus
      	 ,BT.ActiveStatusId 
      ,bt.[CreatedAt]
  FROM [Bank].[BrokerType] BT
  INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID
  WHERE  BT.ActiveStatusId != 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";


            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetBrokerTypeQueryResult>();
                return Result.Ok(response, request, count);
            }

            
        }
    }
    public async Task<GetBrokerTypeQueryResult> GetById(long id)
    {
        var result = new GetBrokerTypeQueryResult();
        string query = @"
                    SELECT DISTINCT BT.[ID]
                                  ,BT.[Name]
                                  ,BT.[Code]
                            	  ,A.Name ActiveStatus
                            	  ,BT.ActiveStatusId 
                              FROM [Bank].[BrokerType] BT
                              INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID WHERE BT.Id = @Id
                    ";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            result = await connection.QueryFirstAsync<GetBrokerTypeQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.NullException;
        }
        return result;
    }
}
