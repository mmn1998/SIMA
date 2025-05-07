using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetCustomFields;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.AssetCustomFields
{
    public class AssetCustomFieldQueryRepository : IAssetCustomFieldQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public AssetCustomFieldQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"Select
                            ACF.Id,
                            ACF.Name,
                            ACF.DisplayName,
                            ACF.AssetTypeId,
                            AT.Name AssetTypeName,
                            ACF.CustomeFieldTypeId,
                            CFT.Name CustomeFieldTypeName,
                            ACF.IsMandetory,
                            ACF.ParentId,
                            ACF.BoundingViewName,
                            ACF.ValueBoundingFeild,
                            ACF.TextBoundingFeild,
                            ACF.ActiveStatusId,
                            A.Name ActiveStatus,
                            ACF.CreatedAt,
                            P.FirstName + ' ' + P.LastName CreatedBy
                            from AssetAndConfiguration.AssetCustomField  ACF
                            join AssetAndConfiguration.AssetType AT on ACF.AssetTypeId = AT.Id and AT.ActiveStatusId <> 3
                            left join AssetAndConfiguration.AssetCustomField Parent on ACF.ParentId = Parent.Id and Parent.ActiveStatusId <> 3
                            join Basic.CustomeFieldType CFT on ACF.CustomeFieldTypeId = CFT.Id and CFT.ActiveStatusId <> 3
                            join Basic.ActiveStatus A on A.ID = ACF.ActiveStatusId 
                            join Authentication.Users U on U.Id = ACF.CreatedBy  and U.ActiveStatusId <> 3
                            join  Authentication.Profile P on U.ProfileID = P.Id and P.ActiveStatusId <> 3
                            where ACF.ActiveStatusId <> 3
";
        }

        public async Task<Result<IEnumerable<GetAssetCustomFieldQueryResult>>> GetAll(GetAllAssetCustomFieldsQuery request)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string queryCount = $@" WITH Query as(
						                    {_mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


            string query = $@" WITH Query as(
							                  {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = (queryCount + query).GenerateQuery(request);
            using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
            var count = await multi.ReadFirstAsync<int>();
            var response = await multi.ReadAsync<GetAssetCustomFieldQueryResult>();
            return Result.Ok(response, request, count);
        }

        public async Task<GetAssetCustomFieldQueryResult> GetById(GetAssetCustomFieldQuery request)
        {
            var query = $@"
                                  Select
                        ACF.Id,
                        ACF.Name,
                        ACF.DisplayName,
                        ACF.AssetTypeId,
                        AT.Name AssetTypeName,
                        ACF.CustomeFieldTypeId,
                        CFT.Name CustomeFieldTypeName,
                        ACF.IsMandetory,
                        ACF.ParentId,
                        ACF.BoundingViewName,
                        ACF.ValueBoundingFeild,
                        ACF.TextBoundingFeild,
                        ACF.ActiveStatusId,
                        A.Name ActiveStatus,
                        ACF.CreatedAt,
                        P.FirstName + ' ' + P.LastName CreatedBy
                        from AssetAndConfiguration.AssetCustomField  ACF
                        join AssetAndConfiguration.AssetType AT on ACF.AssetTypeId = AT.Id and AT.ActiveStatusId <> 3
                        left join AssetAndConfiguration.AssetCustomField Parent on ACF.ParentId = Parent.Id and Parent.ActiveStatusId <> 3
                        join Basic.CustomeFieldType CFT on ACF.CustomeFieldTypeId = CFT.Id and CFT.ActiveStatusId <> 3
                        join Basic.ActiveStatus A on A.ID = ACF.ActiveStatusId 
                        join Authentication.Users U on U.Id = ACF.CreatedBy  and U.ActiveStatusId <> 3
                        join  Authentication.Profile P on U.ProfileID = P.Id and P.ActiveStatusId <> 3
                        where ACF.ActiveStatusId <> 3 and ACF.Id = @Id


                        Select
                        ACFO.OptionText,
                        ACFO.OptionValue
                        from AssetAndConfiguration.AssetCustomField  ACF
                        left join Asset.AssetCustomFieldOption ACFO on ACF.Id = ACFO.AssetCustomFieldId
                        where ACF.ActiveStatusId <> 3 and ACF.Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var multi = await connection.QueryMultipleAsync(query, new { Id = request.Id });
            var response = new GetAssetCustomFieldQueryResult();
            response = await multi.ReadFirstOrDefaultAsync<GetAssetCustomFieldQueryResult>() ?? throw SimaResultException.NotFound;
            response.AssetCustomFieldOptionList = await multi.ReadAsync<GetAssetCustomFieldOption>();
            response.NullCheck();
            return response ?? throw SimaResultException.NotFound;
        }

     
    }
}
