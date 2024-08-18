using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Channels;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Services;

public class ServiceQueryRepository : IServiceQueryRepository
{
    private readonly string _connectionString;
    public ServiceQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetServiceQueryResult>>> GetAll(GetAllServicesQuery request)
    {
        var mainQuery = @"
SELECT S.[Id]
      ,S.[Name]
      ,S.[Code]
      ,S.[ServiceBoundleId]
      ,S.[IsCriticalService]
      ,S.[Description]
      ,S.[ServiceStatusId]
      ,S.[InServiceDate]
      ,S.[TechnicalSupervisorDepartmentId]
      ,S.[ServiceWorkflowDescription] WorkflowDescription
	  ,W.FileContent WorkflowFileContent
      ,S.[Suggestion]
      ,S.[FeedbackUrl]
      ,S.[ActiveStatusId]
      ,S.[CreatedAt]
      ,A.[Name] ActiveStatus
  FROM [ServiceCatalog].[Service] S
  Inner join Basic.ActiveStatus A on A.ID = S.ActiveStatusId
  left join IssueManagement.Issue I on I.SourceId = S.Id and i.ActiveStatusId<>3
  left join Project.WorkFlow W on w.Id = i.CurrentWorkflowId and w.ActiveStatusID<>3
  where s.ActiveStatusId<>3;
";
        string queryCount = $@" WITH Query as(
						                    {mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


        string query = $@" WITH Query as(
							                  {mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();


            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetServiceQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetServiceQueryResult> GetById(long id)
    {
        var response = new GetServiceQueryResult();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
SELECT S.[Id]
      ,S.[Name]
      ,S.[Code]
      ,S.[ServiceBoundleId]
      ,S.[IsCriticalService]
      ,S.[Description]
      ,S.[ServiceStatusId]
      ,S.[InServiceDate]
      ,S.[TechnicalSupervisorDepartmentId]
      ,S.[ServiceWorkflowDescription] WorkflowDescription
	  ,W.FileContent WorkflowFileContent
      ,S.[Suggestion]
      ,S.[FeedbackUrl]
      ,S.[ActiveStatusId]
      ,S.[CreatedAt]
      ,A.[Name] ActiveStatus
  FROM [ServiceCatalog].[Service] S
  Inner join Basic.ActiveStatus A on A.ID = S.ActiveStatusId
  left join IssueManagement.Issue I on I.SourceId = S.Id and i.ActiveStatusId<>3
  left join Project.WorkFlow W on w.Id = i.CurrentWorkflowId and w.ActiveStatusID<>3
  where s.Id = @Id and s.ActiveStatusId<>3;

SELECT 
	sc.ServiceCustomerTypeId CustomerTypeId
	,CT.Name CustomerTypeName
  FROM [ServiceCatalog].[Service] S
  inner join ServiceCatalog.ServiceCustomer SC on s.Id = SC.ServiceId and sc.ActiveStatusId<>3
  inner join Basic.CustomerType CT on SC.ServiceCustomerTypeId = CT.Id and ct.ActiveStatusId<>3
  where s.Id = @Id and s.ActiveStatusId<>3;

SELECT 
	su.ServiceUserTypeId UserTypeId
	,UT.Name UserTypeName
  FROM [ServiceCatalog].[Service] S
  inner join ServiceCatalog.ServiceUser SU on s.Id = Su.ServiceId and su.ActiveStatusId<>3
  inner join Basic.UserType UT on Su.ServiceUserTypeId = UT.Id and UT.ActiveStatusId<>3
  where s.Id = @Id and s.ActiveStatusId<>3;

SELECT 
	PS.PreRequiredServiceId ServiceId
	,PRS.Name ServiceName
  FROM [ServiceCatalog].[Service] S
  inner join ServiceCatalog.PreRequisiteServices PS on s.Id = PS.ServiceId and PS.ActiveStatusId<>3
  inner join ServiceCatalog.Service PRS on PRS.Id = PS.PreRequiredServiceId and PRS.ActiveStatusId<>3
  where s.Id = @Id and s.ActiveStatusId<>3;

SELECT 
	C.Id ProviderId
	,c.Name ProviderName
  FROM [ServiceCatalog].[Service] S
  inner join ServiceCatalog.ServiceProvider SP on s.Id = SP.ServiceId and SP.ActiveStatusId<>3
  inner join Organization.Company C on C.Id = SP.CompanyId and C.ActiveStatusId<>3
  where s.Id = @Id and s.ActiveStatusId<>3;

SELECT 
	R.Name RiskName
	,R.Id RiskId
  FROM [ServiceCatalog].[Service] S
  inner join ServiceCatalog.ServiceRisk SR on s.Id = sr.ServiceId and sr.ActiveStatusId<>3
  inner join RiskManagement.Risk R on R.Id = sr.RiskId and R.ActiveStatusId<>3
  where s.Id = @Id and s.ActiveStatusId<>3;

SELECT 
	Asset.Id AssetId
	,Asset.SerialNumber AssetName
  FROM [ServiceCatalog].[Service] S
  inner join ServiceCatalog.ServiceAsset SA on s.Id = SA.ServiceId and SA.ActiveStatusId<>3
  inner join AssetAndConfiguration.Asset Asset on Asset.Id = SA.AssetId and Asset.ActiveStatusId<>3
  where s.Id = @Id and s.ActiveStatusId<>3;

SELECT 
	SC.ConfigurationItemId
	,CI.Description ConfigurationItemName
  FROM [ServiceCatalog].[Service] S
  inner join ServiceCatalog.ServiceConfigurationItem SC on s.Id = SC.ServiceId and SC.ActiveStatusId<>3
  inner join AssetAndConfiguration.ConfigurationItem CI on CI.Id = SC.ConfigurationItemId and CI.ActiveStatusId<>3
  where s.Id = @Id and s.ActiveStatusId<>3;
";
            using (var multi = await connection.QueryMultipleAsync(query, new { Id = id }))
            {
                response = await multi.ReadFirstOrDefaultAsync<GetServiceQueryResult>();
                response.NullCheck();
                response.ServiceCustomerList = await multi.ReadAsync<GetServiceCustomerQueryResult>();
                response.ServiceUserList = await multi.ReadAsync<GetServiceUserQueryResult>();
                response.ServicePrerequisiteList = await multi.ReadAsync<GetServicePrerequisiteQueryResult>();
                response.ServiceProviderList = await multi.ReadAsync<GetServiceProviderQueryResult>();
                response.ServiceRiskList = await multi.ReadAsync<GetServiceRiskQueryResult>();
                response.ServiceAssetList = await multi.ReadAsync<GetServiceAssetQueryResult>();
                response.ServiceConfigurationItemList = await multi.ReadAsync<GetServiceConfigurationItemQueryResult>();
            }
        }
        return response;
    }
}
