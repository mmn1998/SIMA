using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Application.Query.Contract.Features.BranchManagement.PaymentTypes;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.PaymentTypes;

public class PaymentTypeReadRepository : IPaymentTypeReadRepository
{
    private readonly string _connectionString;

    public PaymentTypeReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetPaymentTypeQueryResult>>> GetAll(GetAllPaymentTypesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @" WITH Query as(
						  SELECT DISTINCT PT.[ID]
      ,PT.[Name]
      ,PT.[Code]
   	    ,A.Name ActiveStatus
   	    ,PT.ActiveStatusId
      ,pt.[CreatedAt]
  FROM [Bank].[PaymentType] PT
  INNER JOIN [Basic].[ActiveStatus] A on A.ID = PT.ActiveStatusID
  WHERE  PT.ActiveStatusId != 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

            string query = $@" WITH Query as(
							SELECT DISTINCT PT.[ID]
      ,PT.[Name]
      ,PT.[Code]
   	    ,A.Name ActiveStatus
   	    ,PT.ActiveStatusId
      ,pt.[CreatedAt]
  FROM [Bank].[PaymentType] PT
  INNER JOIN [Basic].[ActiveStatus] A on A.ID = PT.ActiveStatusID
  WHERE  PT.ActiveStatusId != 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetPaymentTypeQueryResult>();
                return Result.Ok(response, request, count);
            }
        }

    }

    public async Task<GetPaymentTypeQueryResult> GetById(long id)
    {
        var result = new GetPaymentTypeQueryResult();
        string query = @"
SELECT DISTINCT PT.[ID]
      ,PT.[Name]
      ,PT.[Code]
	  ,A.Name ActiveStatus
	  ,PT.ActiveStatusId
  FROM [Bank].[PaymentType] PT
  INNER JOIN [Basic].[ActiveStatus] A on A.ID = PT.ActiveStatusID
  WHERE PT.ID = @Id
";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            result = await connection.QueryFirstOrDefaultAsync<GetPaymentTypeQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.NullException;
        }
        return result;
    }
}
