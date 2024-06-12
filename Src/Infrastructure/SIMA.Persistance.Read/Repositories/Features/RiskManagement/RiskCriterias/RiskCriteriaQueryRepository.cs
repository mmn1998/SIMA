using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskCriterias;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskDegrees;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskImpacts;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevels;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskPossibilities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Resources;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskCriterias
{
    public class RiskCriteriaQueryRepository : IRiskCriteriaQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public RiskCriteriaQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"

                   SELECT DISTINCT R.[Id]
                      ,R.[Code]
                      ,R.[ActiveStatusId]
	                  ,S.[Name] as ActiveStatus,
					  RL.Name DegreeName,
					  RL.Code DegreeCode,
					  RL.Degree,
					  RI.Code ImpactCode , 
					  RI.Name ImpactName,
					  RI.Impact,
					  RP.Code PossibilityCode,
					  RP.Name PossibilityName,
					  RP.Possibility,
                      R.CreatedAt
                  FROM [RiskManagement].[RiskCriteria] R
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = R.ActiveStatusId
				  Left Join RiskManagement.RiskLevel RL on R.RiskLevelId = RL.Id
				  Left Join RiskManagement.RiskImpact RI on R.RiskImpactId = RI.Id
				  Left Join RiskManagement.RiskPossibility RP on R.RiskPossibilityId = RP.Id
                  WHERE R.ActiveStatusId != 3

";
        }

        public async Task<Result<IEnumerable<GetAllRiskCriteriasQueryResult>>> GetAll(GetAllRiskCriteriasQuery request)
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
                    var response = await multi.ReadAsync<GetAllRiskCriteriasQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }
        }

        public async Task<GetRiskCriteriaQueryResult> GetById(long id)
        {
            var response = new GetRiskCriteriaQueryResult();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
              SELECT DISTINCT R.[Id]
                      ,R.[Code]
                      ,R.[ActiveStatusId]
	                  ,S.[Name] as ActiveStatus
                  FROM [RiskManagement].[RiskCriteria] R
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = R.ActiveStatusId
                  WHERE R.ActiveStatusId != 3


                    --RiskDegree

				SELECT DISTINCT 
				      RL.[Id]
				      ,RL.[Code]
					  ,RL.[Name]
                      ,RL.[Degree]
                      ,RL.[CreatedAt]
	                 , S.[Name] as ActiveStatus
                  FROM [RiskManagement].[RiskCriteria] R
				  Left Join RiskManagement.RiskDegree RL on R.RiskLevelId = RL.Id
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = RL.ActiveStatusId
                  WHERE R.ActiveStatusId != 3


				  --RiskImpact

				 SELECT DISTINCT 
				      RI.[Id]
				      ,RI.[Code]
					  ,RI.[Name]
                      ,RI.[Impact]
                      ,RI.[CreatedAt]
	                 , S.[Name] as ActiveStatus
                  FROM [RiskManagement].[RiskCriteria] R
				   Left Join RiskManagement.RiskImpact RI on R.RiskImpactId = RI.Id
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = RI.ActiveStatusId
                  WHERE R.ActiveStatusId != 3

				  --RiskPossibility

				  SELECT DISTINCT 
				      RP.[Id]
				      ,RP.[Code]
					  ,RP.[Name]
                      ,RP.[Possibility]
                      ,RP.[CreatedAt]
	                 , S.[Name] as ActiveStatus
                  FROM [RiskManagement].[RiskCriteria] R
				   Left Join RiskManagement.RiskPossibility RP on R.RiskPossibilityId = RP.Id
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = RP.ActiveStatusId
                  WHERE R.ActiveStatusId != 3"
                ;


                using (var multi = await connection.QueryMultipleAsync(query, new { Id = id }))
                {
                    response = multi.ReadAsync<GetRiskCriteriaQueryResult>().GetAwaiter().GetResult().FirstOrDefault() ?? throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
                    response.RiskDegree = multi.ReadAsync<GetRiskDegreesQueryResult>().GetAwaiter().GetResult().FirstOrDefault();
                    response.Impact = multi.ReadAsync<GetRiskImpactsQueryResult>().GetAwaiter().GetResult().FirstOrDefault();
                    response.Possibility = multi.ReadAsync<GetRiskPossibilitiesQueryResult>().GetAwaiter().GetResult().FirstOrDefault();
                }

                return response ?? throw SimaResultException.NotFound;
            }
        }
    }
}
