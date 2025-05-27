using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemCustomFields;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItemCustomFields
{
    public class ConfigurationItemCustomFieldQueryRepository : IConfigurationItemCustomFieldQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public ConfigurationItemCustomFieldQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"Select
                            CICF.Id,
                            CICF.Name,
                            CICF.DisplayName,
                            CICF.CustomeFieldTypeId,
                            CFT.Name CustomeFieldTypeName,
                            CICF.IsMandetory,
                            CICF.ParentId,
                            CICF.BoundingViewName,
                            CICF.ValueBoundingFeild,
                            CICF.TextBoundingFeild,
                            CICF.ActiveStatusId,
                            A.Name ActiveStatus,
                            CICF.CreatedAt,
                            P.FirstName + ' ' + P.LastName CreatedBy
                            from AssetAndConfiguration.ConfigurationItemCustomField  CICF
                            left join AssetAndConfiguration.ConfigurationItemCustomField Parent on CICF.ParentId = Parent.Id and Parent.ActiveStatusId <> 3
                            join Basic.CustomeFieldType CFT on CICF.CustomeFieldTypeId = CFT.Id and CFT.ActiveStatusId <> 3
                            join Basic.ActiveStatus A on A.ID = CICF.ActiveStatusId 
                            join Authentication.Users U on U.Id = CICF.CreatedBy  and U.ActiveStatusId <> 3
                            join  Authentication.Profile P on U.ProfileID = P.Id and P.ActiveStatusId <> 3
                            where CICF.ActiveStatusId <> 3
";
        }

        public async Task<Result<IEnumerable<GetConfigurationItemCustomFieldQueryResult>>> GetAll(GetAllConfigurationItemCustomFieldsQuery request)
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
            var response = await multi.ReadAsync<GetConfigurationItemCustomFieldQueryResult>();
            return Result.Ok(response, request, count);
        }

        public async Task<GetConfigurationItemCustomFieldQueryResult> GetById(GetConfigurationItemCustomFieldQuery request)
        {
            var query = $@"
                              Select
                                CICF.Id,
                                CICF.Name,
                                CICF.DisplayName,
                                CICF.CustomeFieldTypeId,
                                CFT.Name CustomeFieldTypeName,
                                CICF.IsMandetory,
                                CICF.ParentId,
                                CICF.BoundingViewName,
                                CICF.ValueBoundingFeild,
                                CICF.TextBoundingFeild,
                                CICF.ActiveStatusId,
                                A.Name ActiveStatus,
                                CICF.CreatedAt,
                                P.FirstName + ' ' + P.LastName CreatedBy
                                from AssetAndConfiguration.ConfigurationItemCustomField  CICF
                                left join AssetAndConfiguration.AssetCustomField Parent on CICF.ParentId = Parent.Id and Parent.ActiveStatusId <> 3
                                join Basic.CustomeFieldType CFT on CICF.CustomeFieldTypeId = CFT.Id and CFT.ActiveStatusId <> 3
                                join Basic.ActiveStatus A on A.ID = CICF.ActiveStatusId 
                                join Authentication.Users U on U.Id = CICF.CreatedBy  and U.ActiveStatusId <> 3
                                join  Authentication.Profile P on U.ProfileID = P.Id and P.ActiveStatusId <> 3
                                where CICF.ActiveStatusId <> 3 and CICF.Id = @Id


                                Select
                                ACFO.OptionText,
                                ACFO.OptionValue
                                from AssetAndConfiguration.ConfigurationItemCustomField  CICF
                                left join Asset.ConfigurationItemCustomFieldOption ACFO on CICF.Id = ACFO.ConfigurationItemCustomFieldId
                                where CICF.ActiveStatusId <> 3 and CICF.Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var multi = await connection.QueryMultipleAsync(query, new { Id = request.Id });
            var response = new GetConfigurationItemCustomFieldQueryResult();
            response = await multi.ReadFirstOrDefaultAsync<GetConfigurationItemCustomFieldQueryResult>() ?? throw SimaResultException.NotFound;
            response.AssetCustomFieldOptionList = await multi.ReadAsync<GetConfigurationItemCustomFieldOption>();
            response.NullCheck();
            return response ?? throw SimaResultException.NotFound;
        }
    }
}
