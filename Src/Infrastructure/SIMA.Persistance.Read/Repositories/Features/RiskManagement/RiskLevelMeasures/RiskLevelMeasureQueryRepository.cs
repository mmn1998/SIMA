using ArmanIT.Investigation.Dapper.QueryBuilder;
using Azure;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskImpacts;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelMeasures;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevels;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskPossibilities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Resources;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskLevelMeasures
{
    public class RiskLevelMeasureQueryRepository : IRiskLevelMeasureQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public RiskLevelMeasureQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"

                   SELECT DISTINCT R.[Id]
                      ,R.[Code]
                      ,R.[ActiveStatusId]
	                  ,S.[Name] as ActiveStatus,
					  RL.Name RiskLevelName,
					  RL.Code RiskLevelCode,
					  RL.Level,
					  RI.Code ImpactCode , 
					  RI.Name ImpactName,
					  RI.Impact,
					  RP.Code PossibilityCode,
					  RP.Name PossibilityName,
					  RP.Possibility,
                      R.CreatedAt
                  FROM [RiskManagement].[RiskLevelMeasure] R
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = R.ActiveStatusId
				  Left Join RiskManagement.RiskLevel RL on R.RiskLevelId = RL.Id
				  Left Join RiskManagement.RiskImpact RI on R.RiskImpactId = RI.Id
				  Left Join RiskManagement.RiskPossibility RP on R.RiskPossibilityId = RP.Id
                  WHERE R.ActiveStatusId != 3

";
        }

        public async Task<Result<IEnumerable<GetAllRiskLevelMeasuresQueryResult>>> GetAll(GetAllRiskLevelMeasuresQuery request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
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
                var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
                using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
                {
                    var count = await multi.ReadFirstAsync<int>();
                    var response = await multi.ReadAsync<GetAllRiskLevelMeasuresQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }
        }

        public async Task<GetRiskLevelMeasuresQueryResult> GetById(long id)
        {
            var response = new GetRiskLevelMeasuresQueryResult();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
              SELECT DISTINCT R.[Id]
                      ,R.[Code]
                      ,R.[ActiveStatusId]
	                  ,S.[Name] as ActiveStatus
                  FROM [RiskManagement].[RiskLevelMeasure] R
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = R.ActiveStatusId
                  WHERE R.ActiveStatusId != 3 AND R.[Id] = @Id


                    --RiskLevel

				SELECT DISTINCT 
				      RL.[Id]
				      ,RL.[Code]
					  ,RL.[Name]
                      ,RL.[Level]
                      ,RL.[CreatedAt]
	                 , S.[Name] as ActiveStatus
                  FROM [RiskManagement].[RiskLevelMeasure] R
				  Left Join RiskManagement.RiskLevel RL on R.RiskLevelId = RL.Id
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = RL.ActiveStatusId
                  WHERE R.ActiveStatusId != 3 AND R.[Id] = @Id


				  --RiskImpact

				 SELECT DISTINCT 
				      RI.[Id]
				      ,RI.[Code]
					  ,RI.[Name]
                      ,RI.[Impact]
                      ,RI.[CreatedAt]
	                 , S.[Name] as ActiveStatus
                  FROM [RiskManagement].[RiskLevelMeasure] R
				   Left Join RiskManagement.RiskImpact RI on R.RiskImpactId = RI.Id
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = RI.ActiveStatusId
                  WHERE R.ActiveStatusId != 3 AND R.[Id] = @Id

				  --RiskPossibility

				  SELECT DISTINCT 
				      RP.[Id]
				      ,RP.[Code]
					  ,RP.[Name]
                      ,RP.[Possibility]
                      ,RP.[CreatedAt]
	                 , S.[Name] as ActiveStatus
                  FROM [RiskManagement].[RiskLevelMeasure] R
				   Left Join RiskManagement.RiskPossibility RP on R.RiskPossibilityId = RP.Id
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = RP.ActiveStatusId
                  WHERE R.ActiveStatusId != 3 AND R.[Id] = @Id"
                ;


                using (var multi = await connection.QueryMultipleAsync(query, new { Id = id }))
                {
                    response = multi.ReadAsync<GetRiskLevelMeasuresQueryResult>().GetAwaiter().GetResult().FirstOrDefault() ?? throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
                    response.RiskLevel = multi.ReadAsync<GetRiskLevelsQueryResult>().GetAwaiter().GetResult().FirstOrDefault();
                    response.Impact = multi.ReadAsync<GetRiskImpactsQueryResult>().GetAwaiter().GetResult().FirstOrDefault();
                    response.Possibility = multi.ReadAsync<GetRiskPossibilitiesQueryResult>().GetAwaiter().GetResult().FirstOrDefault();
                }

                return response ?? throw SimaResultException.NotFound;
            }
        }
    }
}
