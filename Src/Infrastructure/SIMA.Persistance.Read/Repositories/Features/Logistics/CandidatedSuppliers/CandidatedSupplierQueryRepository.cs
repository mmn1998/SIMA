using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Logistics.CandidatedSuppliers;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.CandidatedSuppliers;

public class CandidatedSupplierQueryRepository : ICandidatedSupplierQueryRepository
{
    private readonly string _connectionString;
    public CandidatedSupplierQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetCandidatedSupplierQueryResult>>> GetAll(GetAllCandidatedSuppliersQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
  select
	  CS.Id
	  ,s.Name Name
	  ,cs.LogisticsRequestId
	  ,cs.SupplierId
	  ,cs.IsSelected
	  ,CS.SelectionDate
	  ,cs.ActiveStatusId
	  ,a.Name ActiveStatus
      ,cs.CreatedAt
  from Logistics.CandidatedSupplier CS
  inner join Basic.ActiveStatus A on A.ID = cs.ActiveStatusId
  inner join Logistics.Supplier S on s.Id = cs.SupplierId
  where s.ActiveStatusId <> 3 and CS.ActiveStatusId <> 3
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
                var response = await multi.ReadAsync<GetCandidatedSupplierQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<Result<IEnumerable<GetCandidatedSupplierQueryResult>>> GetByLogestictId(long logesticId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
select
  	  CS.Id
  	  ,(s.Name + ' - ' + SR.Name) Name
  	  ,cs.LogisticsSupplyId
  	  ,cs.SupplierId
  	  ,cs.IsSelected
  	  ,CS.SelectionDate
  	  ,cs.ActiveStatusId
  	  ,a.Name ActiveStatus
    ,RI.InquieredPrice
    ,cs.CreatedAt
from Logistics.CandidatedSupplier CS
inner join Basic.ActiveStatus A on A.ID = cs.ActiveStatusId
left join Logistics.RequestInquiry RI on RI.CandidatedSupplierId  = CS.Id
inner join Basic.Supplier S on s.Id = cs.SupplierId
inner join Basic.SupplierRank SR on SR.Id = S.SupplierRankId
where s.ActiveStatusId <> 3 and CS.ActiveStatusId <> 3 and CS.LogisticsSupplyId = @logesticId
							";
            
            using (var multi = await connection.QueryMultipleAsync(mainQuery  , new { logesticId = logesticId }))
            {
                var response = await multi.ReadAsync<GetCandidatedSupplierQueryResult>();
                int index = 1;

                foreach (var item in response)
                {
                    item.Index = index++;
                }
                return Result.Ok(response);
            }

        }
    }

    public async Task<Result<IEnumerable<GetCandidatedSupplierQueryResult>>> GetSelectdSupplierByLogestictId(long logesticId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                    select
  	                      CS.Id
  	                      ,(s.Name + ' - ' + SR.Name) Name
  	                      ,cs.LogisticsSupplyId
  	                      ,cs.SupplierId
  	                      ,cs.IsSelected
  	                      ,CS.SelectionDate
  	                      ,cs.ActiveStatusId
  	                      ,a.Name ActiveStatus
                        ,RI.InquieredPrice
                        ,cs.CreatedAt
                    from Logistics.CandidatedSupplier CS
                    inner join Basic.ActiveStatus A on A.ID = cs.ActiveStatusId
                    left join Logistics.RequestInquiry RI on RI.CandidatedSupplierId  = CS.Id
                    inner join Basic.Supplier S on s.Id = cs.SupplierId
                    inner join Basic.SupplierRank SR on SR.Id = S.SupplierRankId
                    where s.ActiveStatusId <> 3 and CS.ActiveStatusId <> 3 and CS.IsSelected = '1' and  CS.LogisticsSupplyId = @logesticId
							";

            using (var multi = await connection.QueryMultipleAsync(mainQuery, new { logesticId = logesticId }))
            {
                var response = await multi.ReadAsync<GetCandidatedSupplierQueryResult>();
                int index = 1;

                foreach (var item in response)
                {
                    item.Index = index++;
                }
                return Result.Ok(response);
            }

        }
    }
}