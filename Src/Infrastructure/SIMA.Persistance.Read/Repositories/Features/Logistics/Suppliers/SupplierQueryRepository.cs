using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Logistics.Suppliers;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.Suppliers;

public class SupplierQueryRepository : ISupplierQueryRepository
{
    private readonly string _connectionString;
    public SupplierQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetSupplierQueryResult>>> GetAll(GetAllSuppliersQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                               Select 
								 F.[Id]
								,F.[Name]
								,F.[Code]
								,F.MobileNumber
	                            ,F.IsInBlackList
	                            ,F.Address
	                            ,F.FaxNumber
	                            ,F.PhoneNumber
	                            ,F.PostalCode
	                            ,F.SupplierRankId
	                            ,sp.Name SupplierRank
                                ,F.CreatedAt
								,F.[ActiveStatusId]
								,A.[Name] ActiveStatus
								From Logistics.Supplier F
								join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
                                join Logistics.SupplierRank sp on sp.Id = f.SupplierRankId
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
                var response = await multi.ReadAsync<GetSupplierQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetSupplierQueryResult> GetById(GetSupplierQuery request)
    {
        var query = @"
          Select 
			 F.[Id]
			,F.[Name]
			,F.[Code]
			,F.MobileNumber
	        ,F.IsInBlackList
	        ,F.Address
	        ,F.FaxNumber
	        ,F.PhoneNumber
	        ,F.PostalCode
	        ,F.SupplierRankId
	        ,sp.Name SupplierRank
            ,F.CreatedAt
			,F.[ActiveStatusId]
			,A.[Name] ActiveStatus
			From Logistics.Supplier F
			join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
            join Logistics.SupplierRank sp on sp.Id = f.SupplierRankId
          WHERE F.[Id] = @Id AND F.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetSupplierQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }
}