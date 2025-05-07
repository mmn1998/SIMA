using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.WageRates;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Resources;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.WageRates;

public class WageRateQueryRepository : IWageRateQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public WageRateQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT DO.[Id]
	  ,DO.CurrencyOperationTypeId
	  ,CO.Name CurrencyOperationTypeName
	  ,DO.CurrencyTypeId
	  ,CT.Name CurrencyTypeName
	  ,DO.PaymentTypeId
	  ,PT.Name PaymentTypeName
	  ,DO.DraftTypeId
	  ,DT.Name DraftTypeName
	  ,DO.CurrencyPaymentChannelId
	  ,CPT.Name CurrencyPaymentChannelName
	  ,DO.IsBasedOnPercentage
	  ,DO.Discount
	  ,DO.WageFixedValue
	  ,DO.WagePercentage
	  ,DO.Description
	  ,DO.Name
	  ,DOR.Name DraftOriginName
	  ,DOR.Id DraftOriginId
	  ,DO.[CreatedAt]
	  ,A.[Name] ActiveStatus
FROM TrustyDraft.WageRate DO
INNER JOIN [Basic].[ActiveStatus] A ON DO.ActiveStatusId = A.ID
INNER JOIN Bank.CurrencyOprationType CO on Co.Id = DO.CurrencyOperationTypeId and CO.ActiveStatusId<>3
INNER JOIN Bank.CurrencyType CT on CT.Id = DO.CurrencyTypeId and CT.ActiveStatusId<>3
INNER JOIN Bank.PaymentType PT on PT.Id = DO.PaymentTypeId and PT.ActiveStatusId<>3
LEFT join TrustyDraft.DraftOrigin DOR on DOR.Id = DO.DraftOriginId
INNER JOIN TrustyDraft.DraftType DT on DT.Id = DO.DraftTypeId and DT.ActiveStatusId<>3
INNER JOIN TrustyDraft.CurrencyPaymentChannel CPT on CPT.Id = DO.CurrencyPaymentChannelId and CPT.ActiveStatusId<>3
WHERE DO.ActiveStatusId <> 3
";
    }

    public async Task<GetWageCalculatorQueryResult> CalculateWage(GetWageCalculatorQuery request)
    {
        var query = @"
SELECT WR.[Id]
	  ,WR.IsBasedOnPercentage
	  ,WR.Discount
	  ,WR.WagePercentage
	  ,WR.WageFixedValue
FROM TrustyDraft.WageRate WR
where Wr.Id = @Id and WR.CurrencyTypeId = @CurrencyTypeId
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var response = await connection.QueryFirstOrDefaultAsync<GetWageCalculatorQueryResult>(query, new { Id = request.WageRateId, CurrencyTypeId = request.CurrencyTypeId })
            ?? throw new SimaResultException(CodeMessges._100103Code, Messages.WageRateNotMatchCurrencyType);

        if (string.Equals(response.IsBasedOnPercentage, "1", StringComparison.InvariantCultureIgnoreCase))
        {
            response.Wage =  (request.Amount * response.WagePercentage) / 100;
            response.FinalWage = response.Wage - response.Discount;
        }
        else if (string.Equals(response.IsBasedOnPercentage, "0", StringComparison.InvariantCultureIgnoreCase))
        {
            response.Wage = response.WageFixedValue;
            response.FinalWage = response.Wage - response.Discount;
        }
        return response;
    }

    public async Task<Result<IEnumerable<GetWageRateQueryResult>>> GetAll(GetAllWageRatesQuery request)
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
        var response = await multi.ReadAsync<GetWageRateQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<IEnumerable<GetWageRateQueryResult>> GetAllByCurrencyTypeId(long currencyTypeId)
    {
        var query = $@"
          {_mainQuery} AND DO.CurrencyTypeId = @CurrencyTypeId";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryAsync<GetWageRateQueryResult>(query, new { CurrencyTypeId = currencyTypeId });
        result.NullCheck();
        return result;
    }

    public async Task<GetWageRateQueryResult> GetById(GetWageRateQuery request)
    {
        var query = $@"
          {_mainQuery} AND DO.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetWageRateQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;

    }
}