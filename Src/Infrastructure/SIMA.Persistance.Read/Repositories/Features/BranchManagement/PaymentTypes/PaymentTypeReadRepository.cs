using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.PaymentTypes;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Persistance.Read;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.PaymentTypes;

public class PaymentTypeReadRepository : IPaymentTypeReadRepository
{
    private readonly string _connectionString;

    public PaymentTypeReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<List<GetPaymentTypeQueryResult>> GetAll(BaseRequest request)
    {
        var result = new List<GetPaymentTypeQueryResult>();
        string query = string.Empty;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                query = @"
SELECT DISTINCT PT.[ID]
      ,PT.[Name]
      ,PT.[Code]
	  ,A.Name ActiveStatus
	  ,PT.ActiveStatusId
,pt.[CreatedAt]
  FROM [Bank].[PaymentType] PT
  INNER JOIN [Basic].[ActiveStatus] A on A.ID = PT.ActiveStatusID
  WHERE (PT.Name like @SearchValue OR PT.[Code] like @SerachValue)
Order By pt.[CreatedAt] desc  
";
            }
            else
            {
                query = @"
SELECT DISTINCT PT.[ID]
      ,PT.[Name]
      ,PT.[Code]
	  ,A.Name ActiveStatus
	  ,PT.ActiveStatusId
,pt.[CreatedAt]
  FROM [Bank].[PaymentType] PT
  INNER JOIN [Basic].[ActiveStatus] A on A.ID = PT.ActiveStatusID
Order By pt.[CreatedAt] desc  
";
            }
            result = (await connection.QueryAsync<GetPaymentTypeQueryResult>(query, new { SearchValue = "%" + request.SearchValue + "%" }))
                .Skip((request.Skip - 1) * request.Take)
                .Take(request.Take)
                .ToList();
        }
        return result;
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
