using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.AddressTypes;
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

        public async Task<Result<List<GetViewListQueryResult>>> GetAll(GetAllViewListQuery request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var queryCount = @" SELECT  COUNT(*) Result
                                FROM [Authentication].[ViewList]  VL
                                join Basic.ActiveStatus a
                                on VL.ActiveStatusId = a.ID
                               WHERE (@SearchValue is null OR (VL.Name like @SearchValue OR VL.Code like @SearchValue)) AND VL.[ActiveStatusID] <> 3";

                string query = $@"
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
                                WHERE (@SearchValue is null OR (VL.Name like @SearchValue OR VL.Code like @SearchValue)) AND VL.[ActiveStatusID] <> 3
                                order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = (await multi.ReadAsync<GetViewListQueryResult>()).ToList();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }

        public async Task<Result<List<GetViewFieldQueryResult>>> GetAllViewFeild(GetAllViewFieldQuery request)
        {
            if (request.ViewId <= 0)
                throw ViewListExceptions.NotSelectViewList;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var queryCount = @" SELECT  COUNT(*) Result
                                FROM [Authentication].[ViewField]  VF
                                join Basic.ActiveStatus a
                                on VF.ActiveStatusId = a.ID
                               WHERE (@SearchValue is null OR (VF.Name like @SearchValue OR VF.Code like @SearchValue)) AND VF.[ActiveStatusID] <> 3";

                string query = $@"
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
                               WHERE VF.ViewId = @ViewId and (@SearchValue is null OR (VF.Name like @SearchValue OR VF.Code like @SearchValue)) AND VF.[ActiveStatusID] <> 3
                                order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    ViewId = request.ViewId,
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = (await multi.ReadAsync<GetViewFieldQueryResult>()).ToList();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }
}
