using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.PaymentTypes;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
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

            string queryCount = @"
                              SELECT Count(*) Result
                              FROM [Bank].[PaymentType] PT
                              INNER JOIN [Basic].[ActiveStatus] A on A.ID = PT.ActiveStatusID
                              WHERE  PT.ActiveStatusId != 3
                            and (@SearchValue is null OR PT.[Name] like @SearchValue or PT.[Code] like @SearchValue)";

            await connection.OpenAsync();
            string query = $@"
                          SELECT DISTINCT PT.[ID]
                                ,PT.[Name]
                                ,PT.[Code]
                          	    ,A.Name ActiveStatus
                          	    ,PT.ActiveStatusId
                                ,pt.[CreatedAt]
                            FROM [Bank].[PaymentType] PT
                            INNER JOIN [Basic].[ActiveStatus] A on A.ID = PT.ActiveStatusID
                            WHERE  PT.ActiveStatusId != 3
                          and (@SearchValue is null OR PT.[Name] like @SearchValue or PT.[Code] like @SearchValue)
                          order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                          OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                request.Skip,
                request.PageSize
            }))
            {
                var response = await multi.ReadAsync<GetPaymentTypeQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
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
