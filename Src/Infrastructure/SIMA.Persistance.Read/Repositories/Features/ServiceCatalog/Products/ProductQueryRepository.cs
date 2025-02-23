using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Products;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Products;

public class ProductQueryRepository : IProductQueryRepository
{
    private readonly string _connectionString;
    public ProductQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetProductQueryResult>>> GetAll(GetAllProductQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                               select p.Id,
                                p.Name,
                                p.Code,
                                p.Scope,
                                p.Description,
                                p.ProviderCompanyId,
                                c.Name ProviderCompanyName,
                                p.ServiceStatusId,
                                ss.Name ServiceStatusName,
                                P.InServiceDate,
                                P.CreatedAt
                                from [ServiceCatalog].[Product] p
                                join [Organization].[Company] c on c.Id=p.ProviderCompanyId
                                join [ServiceCatalog].[ServiceStatus] ss on ss.Id=p.ServiceStatusId
                                where p.ActiveStatusId!=3
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

            string relatedQuery = @"
--ProductChannel
select c.Id ChannelId,
c.name ChannelName
from [ServiceCatalog].[Product] p
join [ServiceCatalog].[ProductChannel] pc on pc.ProductId=p.Id
join [ServiceCatalog].[Channel] c on pc.ChannelId=c.Id
where p.ActiveStatusId!=3 and pc.ActiveStatusId!=3 and p.Id=@Id
--ProductResponsible
select 
pr.ResponsibleTypeId,
rt.Name ResponsibleTypeName,
s.Id ResponsibleId,
pro.FirstName + SPACE(1) + pro.LastName Responsible
from [ServiceCatalog].[Product] p
join [ServiceCatalog].[ProductResponsible] pr on pr.ProductId=p.Id
join [Organization].[Staff] s on pr.ResponsilbeId=s.Id
join [Authentication].[Profile] pro on pro.Id=s.ProfileId
join [Basic].[ResponsibleType] rt on rt.Id=pr.ResponsibleTypeId
where p.ActiveStatusId!=3 and pr.ActiveStatusId!=3 and rt.ActiveStatusId!=3 and p.Id=@Id
";
            var dynaimcParameters = (queryCount + query).GenerateQuery(request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetProductQueryResult>();
                //foreach (var item in response)
                //{
                //    using (var multiForRelated = await connection.QueryMultipleAsync(relatedQuery, new { Id = item.Id }))
                //    {
                //        item.ProductChannels = (await multi.ReadAsync<ChannelQuery>()).ToList();
                //        item.ProductResponsibles = (await multi.ReadAsync<ProductResponsibleQuery>()).ToList();
                //    }
                //}
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetProductQueryResult> GetById(GetProductQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"
         select p.Id,
                p.Name,
                p.Code,
                p.Scope,
                p.Description,
                p.ProviderCompanyId,
                c.Name ProviderCompanyName,
                p.ServiceStatusId,
                ss.Name ServiceStatusName,
                p.InServiceDate
                from [ServiceCatalog].[Product] p
                join [Organization].[Company] c on c.Id=p.ProviderCompanyId
                join [ServiceCatalog].[ServiceStatus] ss on ss.Id=p.ServiceStatusId
                where p.ActiveStatusId!=3 and p.Id= @Id

            --ProductChannel

            select c.Id ChannelId,
            PC.Id,
            c.name ChannelName
            from [ServiceCatalog].[Product] p
            join [ServiceCatalog].[ProductChannel] pc on pc.ProductId=p.Id
            join [ServiceCatalog].[Channel] c on pc.ChannelId=c.Id
            where p.ActiveStatusId!=3 and pc.ActiveStatusId!=3 and p.Id=@Id

            --ProductResponsible

          select 
      pr.ResponsibleTypeId,
      rt.Name ResponsibleTypeName,
      s.Id ResponsibleId,
		    C.Id CompanyId,
		    C.Name CompanyName,
		    D.Id DepartmentId,
		    D.Name DepartmentName,
      pro.FirstName + SPACE(1) + pro.LastName Responsible,
	  PR.BranchId
	,Br.Name BranchName
  from [ServiceCatalog].[Product] p
  join [ServiceCatalog].[ProductResponsible] pr on pr.ProductId=p.Id
  join [Organization].[Staff] s on pr.ResponsilbeId=s.Id
inner join Authentication.Profile Pro on Pro.Id = S.ProfileId and Pro.ActiveStatusId<>3
inner join Organization.Position PO on PO.Id = S.PositionId and Po.ActiveStatusId <> 3
inner join Organization.Department D on D.Id = PO.DepartmentId and D.ActiveStatusId<>3
inner join Organization.Company C on C.Id = D.CompanyId and C.ActiveStatusId<>3
join [Basic].[ResponsibleType] rt on rt.Id=pr.ResponsibleTypeId
LEFT join Bank.Branch Br on Br.Id = PR.BranchId and Br.ActiveStatusId<>3
where p.ActiveStatusId!=3 and pr.ActiveStatusId!=3 and rt.ActiveStatusId!=3 and p.Id=@Id
";

        using var multi = await connection.QueryMultipleAsync(query, new { request.Id });
        var response = await multi.ReadFirstAsync<GetProductQueryResult>();
        response.ProductChannels = (await multi.ReadAsync<ChannelQuery>()).ToList();
        response.ProductResponsibles = (await multi.ReadAsync<ProductResponsibleQuery>()).ToList();

        return response;
    }
    public async Task<string> GetLastCode()
    {
        var query = @"
select top 1 Code from ServiceCatalog.Product
order by CreatedAt desc
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<string>(query) ?? throw SimaResultException.NotFound;
    }
}
