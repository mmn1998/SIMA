using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Channels;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Channels;

public class ChannelQueryRepository : IChannelQueryRepository
{
    private readonly string _connectionString;
    public ChannelQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetChannelQueryResult>>> GetAll(GetAllChannelsQuery request)
    {
        var mainQuery = @"
SELECT 
		c.[Id]
      ,c.[Name]
      ,c.[Code]
      ,c.[Scope]
      ,c.[Description]
      ,c.[ServiceStatusId]
      ,c.[InServiceDate]
      ,c.[CreatedAt]
	  ,a.[Name] ActiveStatus
  FROM [ServiceCatalog].[Channel] C
  inner join Basic.ActiveStatus A on c.ActiveStatusId = a.ID and C.ActiveStatusId <> 3
  where c.ActiveStatusId<>3;
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
                var response = await multi.ReadAsync<GetChannelQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetChannelQueryResult> GetById(long id)
    {
        var response = new GetChannelQueryResult();
        var query = @"
SELECT 
		c.[Id]
      ,c.[Name]
      ,c.[Code]
      ,c.[Scope]
      ,c.[Description]
      ,c.[ServiceStatusId]
      ,c.[InServiceDate]
      ,c.[CreatedAt]
	  ,a.[Name] ActiveStatus
  FROM [ServiceCatalog].[Channel] C
  inner join Basic.ActiveStatus A on c.ActiveStatusId = a.ID and C.ActiveStatusId <> 3
  where c.Id = @Id and c.ActiveStatusId<>3;

SELECT 
		c.Id ChannelId
		,cr.ResponsibleTypeId
		,RT.[Name] ResponsibleTypeName
		,CR.ResponsibleId
		,(p.FirstName + ' ' + p.LastName) ResponsibleFullName
  FROM [ServiceCatalog].[Channel] C
  inner join ServiceCatalog.ChannelResponsible CR on cr.ChannelId = c.Id and CR.ActiveStatusId <> 3
  inner join Basic.ResponsibleType RT on RT.Id = CR.ResponsibleTypeId and rt.ActiveStatusId<>3
  inner join Organization.Staff S on s.Id = cr.ResponsibleId and s.ActiveStatusId<>3
  inner join Authentication.Profile P on s.ProfileId = p.Id and p.ActiveStatusId<>3
  where c.Id = @Id and c.ActiveStatusId<>3;

SELECT 
		c.Id ChannelId
		,PC.ProductId
		,p.[Name] ProductName
  FROM [ServiceCatalog].[Channel] C
  inner join ServiceCatalog.ProductChannel PC on Pc.ChannelId = c.Id and pc.ActiveStatusId<>3
  inner join ServiceCatalog.Product P on P.Id = Pc.ProductId
  where c.Id = @Id and c.ActiveStatusId<>3;

SELECT 

		c.Id ChannelId
		,CUT.UserTypeId
		,UT.[Name] UserTypeName
  FROM [ServiceCatalog].[Channel] C
  inner join ServiceCatalog.ChannelUserType CUT on CUT.ChannelId = c.Id and CUT.ActiveStatusId<>3
  inner join Basic.UserType UT on ut.Id = CUT.UserTypeId
  where c.Id = @Id and c.ActiveStatusId<>3;

SELECT 
        c.Id ChannelId
		,cap.IpAddress
		,cap.Port
  FROM [ServiceCatalog].[Channel] C
  inner join ServiceCatalog.ChannelAccessPoint CAP on CAP.ChannelId = c.Id and CAP.ActiveStatusId<>3
  where c.Id = @Id and c.ActiveStatusId<>3
";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var multi = await connection.QueryMultipleAsync(query, new { Id = id}))
            {
                response = await multi.ReadFirstOrDefaultAsync<GetChannelQueryResult>();
                response.NullCheck();
                response.ChannelResponsibleList = await multi.ReadAsync<GetChannelResponsibleQuery>();
                response.ProductChannelList = await multi.ReadAsync<GetProductChannelQuery>();
                response.ChannelUserTypeList = await multi.ReadAsync<GetChannelUserTypeQuery>();
                response.ChannelAccessPointList = await multi.ReadAsync<GetChannelAccessPointQuery>();
            }

        }
        return response;
    }
}
