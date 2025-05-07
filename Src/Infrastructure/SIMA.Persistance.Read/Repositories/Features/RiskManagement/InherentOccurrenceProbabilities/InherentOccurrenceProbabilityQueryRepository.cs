using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.InherentOccurrenceProbabilities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.InherentOccurrenceProbabilities;

public class InherentOccurrenceProbabilityQueryRepository : IInherentOccurrenceProbabilityQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public InherentOccurrenceProbabilityQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT
	IOP.[Id]
    ,IOP.[Code]
	,IOP.ScenarioHistoryId
	,SH.ValueTitle ScenarioHistoryName
	,IOP.MatrixAValueId
	,MV.ValueTitle MatrixAValueName
	,IOP.InherentOccurrenceProbabilityValueId
	,IOPV.Name InherentOccurrenceProbabilityValueName
	,A.[Name] ActiveStatus
    ,IOP.CreatedAt
FROM [RiskManagement].[InherentOccurrenceProbability] IOP
INNER JOIN [Basic].[ActiveStatus] A ON IOP.ActiveStatusId = A.ID
INNER JOIN RiskManagement.ScenarioHistory SH on SH.Id = IOP.ScenarioHistoryId AND SH.ActiveStatusId<>3
INNER JOIN RiskManagement.MatrixAValue MV on MV.Id = IOP.MatrixAValueId AND MV.ActiveStatusId<>3
INNER JOIN RiskManagement.InherentOccurrenceProbabilityValue IOPV on IOPV.Id = IOP.InherentOccurrenceProbabilityValueId AND IOPV.ActiveStatusId<>3
WHERE IOP.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetInherentOccurrenceProbabilityQueryResult>>> GetAll(GetAllInherentOccurrenceProbabilitiesQuery request)
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
        var response = await multi.ReadAsync<GetInherentOccurrenceProbabilityQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetInherentOccurrenceProbabilityQueryResult> GetById(GetInherentOccurrenceProbabilityQuery request)
    {
        var query = $@"
          {_mainQuery} AND IOP.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetInherentOccurrenceProbabilityQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}