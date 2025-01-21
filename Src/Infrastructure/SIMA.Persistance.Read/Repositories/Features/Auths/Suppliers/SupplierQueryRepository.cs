using ArmanIT.Investigation.Dapper.QueryBuilder;
using Azure;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Profiles;
using SIMA.Application.Query.Contract.Features.Auths.Roles;
using SIMA.Application.Query.Contract.Features.Auths.Suppliers;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Suppliers;

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
	                            ,F.IsInBlackList
	                            ,F.SupplierRankId
	                            ,sp.Name SupplierRank
                                ,F.CreatedAt
								,F.[ActiveStatusId]
								,A.[Name] ActiveStatus
								From Basic.Supplier F
								join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
                                join Basic.SupplierRank sp on sp.Id = f.SupplierRankId
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

    public async Task<Result<IEnumerable<GetAllOrderedNotInBlackListSuppliersQueryResult>>> GetAllOrderedNotInBlackList(GetAllOrderedNotInBlackListSuppliersQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            request.Sorts = new List<SortModel>
            {
               new SortModel{ Key = "Ordering", Type = "asc"}
            };

            string mainQuery = @"select
            s.Id 
            ,s.Id SupplierId
            ,sr.Id SupplierRankId
            ,s.Name Name
            ,s.Name SupplierName
            ,sr.Name SupplierRankName
            ,sr.Ordering
            ,s.CreatedAt
            from basic.Supplier s
            inner join Basic.SupplierRank sr on sr.Id = s.SupplierRankId
        where s.IsInBlackList <> '1' and S.ActiveStatusId <> 3
        ";
            string queryCount = $@"
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
                var response = await multi.ReadAsync<GetAllOrderedNotInBlackListSuppliersQueryResult>();
                int index = 1;
                if (request.Page > 1)
                {
                    var page = request.Page - 1;
                    var pagination = request.PageSize * page + 1;
                    index = pagination;
                }

                foreach (var item in response)
                {
                    item.Index = index++;
                }
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetSupplierQueryResult> GetById(GetSupplierQuery request)
    {
        var response = new GetSupplierQueryResult();
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = @"
                   Select 
                        F.[Id]
                       ,F.[Name]
                       ,F.[Code]
                       ,F.IsInBlackList
                       ,F.SupplierRankId
                       ,sp.Name SupplierRank
                       ,F.CreatedAt
                       ,F.[ActiveStatusId]
                       ,A.[Name] ActiveStatus
                    From basic.Supplier F
                    join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
                    join basic.SupplierRank sp on sp.Id = f.SupplierRankId
                    WHERE F.[Id] = @Id AND F.ActiveStatusId <> 3


                     select 
                         SAB.Id AddressBookId
                        ,SAB.AddressTypeId
                        ,AT.Name AddressTypeName
                        ,SAB.PostalCode
                        ,SAB.Address
                     from basic.Supplier S
                     join basic.SupplierAddressBook SAB on S.Id = SAB.SupplierId and  SAB.ActiveStatusId !=3
                     join Basic.AddressType AT on AT.Id = SAB.AddressTypeId and  AT.ActiveStatusId !=3
                     WHERE S.[Id] = @Id AND S.ActiveStatusId <> 3

                      select 
                         SPB.Id PhoneBookId
                        ,SPB.PhoneTypeId
                        ,PT.Name PhoneTypeName
                        ,SPB.PhoneNumber
                     from basic.Supplier S
                     join basic.SupplierPhoneBook SPB on S.Id = SPB.SupplierId and  SPB.ActiveStatusId !=3
                     join Basic.PhonType PT on PT.Id = SPB.PhoneTypeId and  PT.ActiveStatusId !=3
                     WHERE S.[Id] = @Id AND S.ActiveStatusId <> 3


                    select 
                         SAL.Id AccountListId
                        ,SAL.IBAN
                     from basic.Supplier S
                     join basic.SupplierAccountList SAL on S.Id = SAL.SupplierId and  SAL.ActiveStatusId !=3
                     WHERE S.[Id] = @Id AND S.ActiveStatusId <> 3"
        ;
            using (var multi = await connection.QueryMultipleAsync(query, new { Id = request.Id }))
            {
                response = multi.ReadAsync<GetSupplierQueryResult>().GetAwaiter().GetResult().Single();
                response.AddressBooks = multi.ReadAsync<GetSupplierAddressBookQuery>().GetAwaiter().GetResult().ToList();
                response.PhoneBooks = multi.ReadAsync<GetSupplierPhoneBookQuery>().GetAwaiter().GetResult().ToList();
                response.AccountList = multi.ReadAsync<GetSupplierAccountListQuery>().GetAwaiter().GetResult().ToList();
            }
            return response;
        }

    }

    public async Task<Result<IEnumerable<GetSupplierAccountByLogisticsSupplyQueryResult>>> GetSupplierAccountByLogisticsSupply(long logisticsSupplyId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
               

            string mainQuery = @"select 
                            LS.Id LogisticsSupplyId,
                            CS.Id CandidatedSupplier,
                            S.Id SupplierId,
                            SAL.IBAN Name,
                            SAL.Id
                            from Logistics.LogisticsSupply Ls
                            join Logistics.CandidatedSupplier CS on Ls.Id = CS.LogisticsSupplyId and CS.ActiveStatusId <> 3
                            join Basic.Supplier S on CS.SupplierId = S.Id and CS.ActiveStatusId <> 3
                            join Basic.SupplierAccountList SAL on S.Id = SAL.SupplierId and SAL.ActiveStatusId <> 3
                            where Ls.Id = @Id  and LS.ActiveStatusId<> 3
        ";

            var result = await connection.QueryAsync<GetSupplierAccountByLogisticsSupplyQueryResult>(mainQuery, new { Id = logisticsSupplyId });
            
            return Result.Ok(result);
        }
    }
}