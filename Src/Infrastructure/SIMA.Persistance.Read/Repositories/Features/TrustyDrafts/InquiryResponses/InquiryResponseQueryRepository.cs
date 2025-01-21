using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryResponses;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.InquiryResponses;

public class InquiryResponseQueryRepository : IInquiryResponseQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public InquiryResponseQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT 
                                IR.Id,
	                            IR.ExcessWage,
                                --IR.ValidityPeriod,
                                IR.CalculatedWage,
                                IR.InquiryRequestId,
	                            IReq.ApplicantName,
	                            IReq.BeneficiaryName,
	                            IReq.Amount,
                                IR.BrokerInquiryStatusId,
                                BIS.Name AS BrokerInquiryStatusName,
                                IR.BrokerId,
                                B.Name AS BrokerName,
                                IR.WageRateId,
                                WR.Name AS WageRateName,
	                            WR.Description AS WageRateDescription,
	                            WR.Description AS WageRateCurrentBalance,
	                            WR.Description AS WageRateVagePrecentage,
                                TD.DraftNumber,
                                A.Name ActiveStatus,
                                IR.CreatedAt,
                                P.FirstName + ' ' + P.LastName AS CreatedBy
                            FROM TrustyDraft.InquiryResponse IR
                            JOIN TrustyDraft.InquiryRequest IReq on IR.InquiryRequestId = IReq.Id
                            JOIN Bank.Broker B ON B.Id = IR.BrokerId AND B.ActiveStatusId <> 3
                            JOIN TrustyDraft.BrokerInquiryStatus BIS ON BIS.Id = IR.BrokerInquiryStatusId AND BIS.ActiveStatusId <> 3
                            left join TrustyDraft.TrustyDraft TD on TD.InquiryRequestId = IReq.Id and TD.ActiveStatusId <> 3
                            JOIN TrustyDraft.WageRate WR ON WR.Id = IR.WageRateId AND WR.ActiveStatusId <> 3
                            JOIN Authentication.Users U ON U.Id = IR.CreatedBy AND U.ActiveStatusId <> 3
                            JOIN Authentication.Profile P ON P.Id = U.ProfileID
                            Join Basic.ActiveStatus A on IR.ActiveStatusId = A.ID
                            WHERE IR.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetInquiryResponseQueryResult>>> GetAll(GetAllInquiryResponsesQuery request)
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
            var dynaimcParameters = (queryCount + query).GenerateQuery(request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetInquiryResponseQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<Result<IEnumerable<GetInquiryResponseQueryResult>>> GetAllAcceptResponse()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = $@"
select 
    IR.Id,
    IR.ExcessWage,
    IR.CalculatedWage,
    IR.InquiryRequestId,
    IReq.ReferenceNumber,
    IReq.BeneficiaryName,
    IRC.Amount,
    IR.BrokerInquiryStatusId,
    IR.BrokerId,
    IR.WageRateId,
    IR.CreatedAt
from TrustyDraft.InquiryResponse IR
join TrustyDraft.InquiryRequest IReq on IR.InquiryRequestId = IReq.Id  and IReq.ActiveStatusId <> 3
left join TrustyDraft.TrustyDraft TD on TD.InquiryRequestId = IReq.Id  and TD.ActiveStatusId <> 3
left join TrustyDraft.InquiryRequestCurrency IRC on IRC.Id = IR.InquiryRequestCurrencyId and IRC.ActiveStatusId<>3
where (Ir.BrokerInquiryStatusId = 1 or IR.BrokerInquiryStatusId = 3)
        and TD.InquiryRequestId is null and IR.ActiveStatusId <> 3
    order by Ir.CreatedAt desc
";
            using (var multi = await connection.QueryMultipleAsync(query))
            {
                var response = await multi.ReadAsync<GetInquiryResponseQueryResult>();
                return Result.Ok(response);
            }
        }
    }

    public async Task<GetInquiryResponseQueryResult> GetById(GetInquiryResponseQuery request)
    {
        try
        {
            var query = $@"
                 {_mainQuery} AND IR.[Id] = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstAsync<GetInquiryResponseQueryResult>(query, new { Id=  request.Id });
                result.NullCheck();
                return result ?? throw SimaResultException.NotFound;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        
    }
}
