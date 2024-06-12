using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.AddressTypes;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Application.Query.Contract.Features.Auths.ViewLists;
using SIMA.Application.Query.Contract.Features.Auths.ViewLists.ViewField;
using SIMA.Domain.Models.Features.Auths.ViewLists.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.ViewLists
{
    public class ViewListQueryRepositorty : IViewListQueryRepositorty
    {
        private readonly string _connectionString;

        public ViewListQueryRepositorty(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }

        public async Task<Result<IEnumerable<GetViewListQueryResult>>> GetAll(GetAllViewListQuery request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var queryCount = @"  WITH Query as(
						  SELECT DISTINCT 
     VL.[ID] as Id
    ,VL.[Name]
    ,VL.[Code]
    ,a.ID ActiveStatusId
    ,a.Name ActiveStatus
    ,VL.[CreatedAt]
FROM [Authentication].[ViewList]  VL
     join Basic.ActiveStatus a
     on VL.ActiveStatusId = a.ID
 WHERE  VL.[ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

                string query = $@" WITH Query as(
							  SELECT DISTINCT 
     VL.[ID] as Id
    ,VL.[Name]
    ,VL.[Code]
    ,a.ID ActiveStatusId
    ,a.Name ActiveStatus
    ,VL.[CreatedAt]
FROM [Authentication].[ViewList]  VL
     join Basic.ActiveStatus a
     on VL.ActiveStatusId = a.ID
 WHERE  VL.[ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
                var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);

                using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
                {
                    var count = await multi.ReadFirstAsync<int>();
                    var response = await multi.ReadAsync<GetViewListQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }
        }

        public async Task<Result<IEnumerable<GetViewFieldQueryResult>>> GetAllViewFeild(GetAllViewFieldQuery request)
        {
            if (request.ViewId <= 0)
                throw ViewListExceptions.NotSelectViewList;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var queryCount = @" WITH Query as(
						  SELECT DISTINCT 
     VF.[ID] as Id
    ,VL.[ID] as ViewId
    ,VL.[Name] as ViewName
    ,VF.[Name]
    ,VF.[Code]
    ,a.ID ActiveStatusId
    ,a.Name ActiveStatus
    ,VF.[CreatedAt]
FROM [Authentication].[ViewField]  VF
join Basic.ActiveStatus a on VF.ActiveStatusId = a.ID
join [Authentication].[ViewList] VL on VL.Id = VF.ViewId
WHERE VF.ViewId = @ViewId  AND VF.[ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

                string query = $@" WITH Query as(
							  SELECT DISTINCT 
     VF.[ID] as Id
    ,VL.[ID] as ViewId
    ,VL.[Name] as ViewName
    ,VF.[Name]
    ,VF.[Code]
    ,a.ID ActiveStatusId
    ,a.Name ActiveStatus
    ,VF.[CreatedAt]
FROM [Authentication].[ViewField]  VF
join Basic.ActiveStatus a on VF.ActiveStatusId = a.ID
join [Authentication].[ViewList] VL on VL.Id = VF.ViewId
WHERE VF.ViewId = @ViewId  AND VF.[ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
                var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
                dynaimcParameters.Item2.Add("ViewId", request.ViewId);
                using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
                {
                    var count = await multi.ReadFirstAsync<int>();
                    var response = await multi.ReadAsync<GetViewFieldQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }
        }
    }
}
