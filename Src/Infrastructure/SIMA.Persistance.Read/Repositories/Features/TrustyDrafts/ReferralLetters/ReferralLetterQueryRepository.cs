using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.ReferalLetters;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Resources;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.ReferralLetters;

public class ReferralLetterQueryRepository : IReferralLetterQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ReferralLetterQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"select 
	                        L.Id,
                            L.LetterNumber,
                            L.LetterDate,
                            L.LetterDocumentId,
                            L.TrustyDraftId,
							TD.DraftNumber,
							TD.DraftNumberBasedOnOrder,
							Br.Id BranchId,
							Br.Name BranchName,
							BR.Code BranchCode,
	                        B.Id BrokerId,
                            B.Name BrokerName,
                            B.Code BrokerCode,
							C.Id CustomerId,
							C.Name CustomerName,
							C.CustomerNumber,
							TD.DraftIssueDate,
							TD.DraftRequestAmount,
							TD.DraftRequestAmountBasedOnUsd,
							CT.Id CurrencyTypeId,
							CT.Name CurrencyTypeName,
							CT.Code CurrencyTypeCode,
							PT.Id PaymentTypeId,
							PT.Name PaymentTypeName,
							DS.Id DraftStatusId,
							DS.Name DraftStatusName,
							S.Name DraftIssueCurrentStepName,
							I.Id IssueId,
							I.Code IssueCode,
                            L.CreatedAt AS CreatedAt,
							BT.Id BrokerTypeId,
							BT.Name BrokerTypeName,
                            P.FirstName + ' ' + LastName   AS CreatedBy
                        from TrustyDraft.ReferralLetter L
						join TrustyDraft.TrustyDraft TD on TD.Id = L.TrustyDraftId  and TD.ActiveStatusId <> 3
						join IssueManagement.Issue I on I.Id = TD.IssueId and I.ActiveStatusId <>3 
						join Project.Step S on S.Id = I.CurrenStepId and S.ActiveStatusID <> 3
						join Bank.Branch Br on Br.Id = TD.BranchId  and Br.ActiveStatusId <> 3
						join Bank.Customer C on c.Id = TD.CustomerId and C.ActiveStatusId <> 3
						left join Bank.CurrencyType CT on CT.Id = TD.DraftCurrencyTypeId and CT.ActiveStatusId <> 3
                        join Authentication.Users U on U.Id = L.CreatedBy  and U.ActiveStatusId <> 3
                        join Authentication.Profile P on U.ProfileID = P.Id and P.ActiveStatusId <> 3
                        join Bank.Broker B on B.Id = L.BrokerId and B.ActiveStatusId <> 3
						left join Bank.PaymentType PT on PT.Id = TD.PaymentTypeId and PT.ActiveStatusId<> 3
						left join TrustyDraft.DraftStatus DS on DS.Id = TD.DraftStatusId and DS.ActiveStatusId <> 3
						left join Bank.BrokerType BT on BT.Id =  TD.BrokerTypeId and BT.ActiveStatusId <> 3
                        where L.ActiveStatusId <>3   ";
    }

    public async Task<Result<IEnumerable<GetAllReferralLettersQueryResult>>> GetAll(GetAllReferalLettersQuery request)
    {
        try
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
            var response = await multi.ReadAsync<GetAllReferralLettersQueryResult>();
            return Result.Ok(response, request, count);
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    public async Task<Result<IEnumerable<GetAllReferralLettersQueryResult>>> GetAllToExchange(GetAllReferralLetterToExchangeQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        string queryCount = $@" WITH Query as(
						                    {_mainQuery} AND BT.Id = 2 --  صرافی
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
        var response = await multi.ReadAsync<GetAllReferralLettersQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<Result<IEnumerable<GetAllReferralLettersQueryResult>>> GetAllToSecretariat(GetAllReferralLetterToSecretariatQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        string queryCount = $@" WITH Query as(
						                    {_mainQuery} AND BT.Id <> 2 -- غیر صرافی یا راهبر یا دبیرخانه
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
        var response = await multi.ReadAsync<GetAllReferralLettersQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetReferalLetterQueryResult> GetById(GetReferalLetterQuery request)
    {
        try
        {
            var response = new GetReferalLetterQueryResult();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string query = @"
                                    select 
                                        L.LetterNumber,
                                        L.LetterDate,
                                        L.LetterDocumentId,
                                        L.CreatedAt AS LetterCreatedAt,
                                        P.FirstName + ' ' + LastName   AS LetterCreatedBy
                                    from TrustyDraft.ReferralLetter L
                                    join Authentication.Users U on U.Id = L.CreatedBy 
                                    join Authentication.Profile P on U.ProfileID = P.Id
                                    where L.ActiveStatusId <>3 and L.Id = @Id

                                    ------ broker
                                    select 
                                        B.Id BrokerId,
                                        B.Name BrokerName,
                                        B.Code BrokerCode
                                    from TrustyDraft.ReferralLetter L
                                    join Bank.Broker B on B.Id = L.BrokerId and B.ActiveStatusId <> 3
                                    where L.ActiveStatusId <>3 and L.Id = @Id

                                    --------trustyDraft

                                    select 
                                        TD.Id TrustyDraftId,
                                        TD.DraftNumber,
                                        TD.DraftNumberBasedOnOrder,
                                        TD.DraftIssueDate,
                                        TD.DraftRequestAmount,
                                        TD.DraftRequestAmountBasedOnUsd,
                                        TD.CreatedAt AS CreatedAt,
                                        P.FirstName + ' ' + LastName AS CreatedBy
                                    from TrustyDraft.ReferralLetter L
                                    join TrustyDraft.ReferralLetterDraftList LTD on LTD.ReferralLetterId = L.Id and LTD.ActiveStatusId<>3
                                    join TrustyDraft.TrustyDraft TD on TD.Id = LTD.TrustyDraftId and TD.ActiveStatusId<>3
                                    join Authentication.Users U on U.Id = TD.CreatedBy 
                                    join Authentication.Profile P on U.ProfileID = P.Id
                                    where L.ActiveStatusId <>3 and L.Id = @Id

                                    -------branch

                                    select 
	                                    TD.Id TrustyDraftId,
	                                    Br.Id BranchId,
                                        Br.Name BranchName,
                                        Br.Code BranchCode
                                    from TrustyDraft.ReferralLetter L
                                    join TrustyDraft.ReferralLetterDraftList LTD on LTD.ReferralLetterId = L.Id and LTD.ActiveStatusId<>3
                                    join TrustyDraft.TrustyDraft TD on TD.Id = LTD.TrustyDraftId  and TD.ActiveStatusId<>3
                                    join Bank.Branch BR on TD.BranchId = BR.Id and BR.ActiveStatusId <> 3
                                    where L.ActiveStatusId <>3 and L.Id = @Id


                                    -------Customer

                                    select 
	                                    TD.Id TrustyDraftId,
                                        C.Id CustomerId,
                                        C.Name CustomerName,
                                        C.CustomerNumber
                                    from TrustyDraft.ReferralLetter L
                                    join TrustyDraft.ReferralLetterDraftList LTD on LTD.ReferralLetterId = L.Id and LTD.ActiveStatusId<>3
                                    join TrustyDraft.TrustyDraft TD on TD.Id = LTD.TrustyDraftId  and TD.ActiveStatusId<>3
                                    join Bank.Customer C on C.Id = TD.CustomerId and C.ActiveStatusId <> 3
                                    where L.ActiveStatusId <>3 and L.Id = @Id


                                    -------CurrencyType

                                    select 
	                                    TD.Id TrustyDraftId,
                                        C.Id CurrencyTypeId,
                                        C.Name CurrencyTypeName,
                                        C.Code CurrencyTypeCode
                                    from TrustyDraft.ReferralLetter L
                                    join TrustyDraft.ReferralLetterDraftList LTD on LTD.ReferralLetterId = L.Id and LTD.ActiveStatusId<>3
                                    join TrustyDraft.TrustyDraft TD on TD.Id = LTD.TrustyDraftId  and TD.ActiveStatusId<>3
                                    join Bank.CurrencyType C on C.Id = TD.CustomerId and C.ActiveStatusId <> 3
                                    where L.ActiveStatusId <>3 and L.Id = @Id

";


            using var multi = await connection.QueryMultipleAsync(query, new { Id = request.Id });
            response = multi.ReadAsync<GetReferalLetterQueryResult>().GetAwaiter().GetResult()?.FirstOrDefault();
            if (response is null) throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
            response.Borker = multi.ReadAsync<GetBroker>().GetAwaiter().GetResult().FirstOrDefault();
            response.TrustyDraftList = multi.ReadAsync<GetTrustyDraft>().GetAwaiter().GetResult().ToList();
            var branch = multi.ReadAsync<Application.Query.Contract.Features.TrustyDrafts.ReferalLetters.GetBranch>().GetAwaiter().GetResult().FirstOrDefault();
            var customer = multi.ReadAsync<Application.Query.Contract.Features.TrustyDrafts.ReferalLetters.GetCustomer>().GetAwaiter().GetResult().FirstOrDefault();
            var currencyType = multi.ReadAsync<Application.Query.Contract.Features.TrustyDrafts.ReferalLetters.GetDraftCurrencyType>().GetAwaiter().GetResult().FirstOrDefault();

            if (response.TrustyDraftList is not null)
            {
                foreach (var item in response.TrustyDraftList)
                {
                    item.Branch = branch;
                    item.Customer = customer;
                    item.DraftCurrencyType = currencyType;

                }
                response.NullCheck();
            }
            return response;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<GetReferalLetterQueryResult> GetByLetterNumber(string letterNumber)
    {
        var response = new GetReferalLetterQueryResult();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        string query = @"
                                    select 
                                        L.LetterNumber,
                                        L.LetterDate,
                                        L.LetterDocumentId,
                                        L.CreatedAt AS LetterCreatedAt,
                                        P.FirstName + ' ' + LastName   AS LetterCreatedBy
                                    from TrustyDraft.ReferralLetter L
                                    join Authentication.Users U on U.Id = L.CreatedBy 
                                    join Authentication.Profile P on U.ProfileID = P.Id
                                    where L.ActiveStatusId <>3 and L.LetterNumber = @LetterNumber

                                    ------ broker
                                    select 
                                        B.Id BrokerId,
                                        B.Name BrokerName,
                                        B.Code BrokerCode
                                    from TrustyDraft.ReferralLetter L
                                    join Bank.Broker B on B.Id = L.BrokerId and B.ActiveStatusId <> 3
                                    where L.ActiveStatusId <>3 and L.LetterNumber = @LetterNumber

                                    --------trustyDraft

                                    select 
                                        TD.Id TrustyDraftId,
                                        TD.DraftNumber,
                                        TD.DraftNumberBasedOnOrder,
                                        TD.DraftIssueDate,
                                        TD.DraftRequestAmount,
                                        TD.DraftRequestAmountBasedOnUsd,
                                        TD.CreatedAt AS CreatedAt,
                                        P.FirstName + ' ' + LastName AS CreatedBy
                                    from TrustyDraft.ReferralLetter L
                                    join TrustyDraft.ReferralLetterDraftList LTD on LTD.ReferralLetterId = L.Id and LTD.ActiveStatusId<>3
                                    join TrustyDraft.TrustyDraft TD on TD.Id = LTD.TrustyDraftId and TD.ActiveStatusId<>3
                                    join Authentication.Users U on U.Id = TD.CreatedBy 
                                    join Authentication.Profile P on U.ProfileID = P.Id
                                    where L.ActiveStatusId <>3 and L.LetterNumber = @LetterNumber

                                    -------branch

                                    select 
	                                    TD.Id TrustyDraftId,
	                                    Br.Id BranchId,
                                        Br.Name BranchName,
                                        Br.Code BranchCode
                                    from TrustyDraft.ReferralLetter L
                                    join TrustyDraft.ReferralLetterDraftList LTD on LTD.ReferralLetterId = L.Id and LTD.ActiveStatusId<>3
                                    join TrustyDraft.TrustyDraft TD on TD.Id = LTD.TrustyDraftId  and TD.ActiveStatusId<>3
                                    join Bank.Branch BR on TD.BranchId = BR.Id and BR.ActiveStatusId <> 3
                                    where L.ActiveStatusId <>3 and L.LetterNumber = @LetterNumber


                                    -------Customer

                                    select 
	                                    TD.Id TrustyDraftId,
                                        C.Id CustomerId,
                                        C.Name CustomerName,
                                        C.CustomerNumber
                                    from TrustyDraft.ReferralLetter L
                                    join TrustyDraft.ReferralLetterDraftList LTD on LTD.ReferralLetterId = L.Id and LTD.ActiveStatusId<>3
                                    join TrustyDraft.TrustyDraft TD on TD.Id = LTD.TrustyDraftId  and TD.ActiveStatusId<>3
                                    join Bank.Customer C on C.Id = TD.CustomerId and C.ActiveStatusId <> 3
                                    where L.ActiveStatusId <>3 and L.LetterNumber = @LetterNumber


                                    -------CurrencyType

                                    select 
	                                    TD.Id TrustyDraftId,
                                        C.Id CurrencyTypeId,
                                        C.Name CurrencyTypeName,
                                        C.Code CurrencyTypeCode
                                    from TrustyDraft.ReferralLetter L
                                    join TrustyDraft.ReferralLetterDraftList LTD on LTD.ReferralLetterId = L.Id and LTD.ActiveStatusId<>3
                                    join TrustyDraft.TrustyDraft TD on TD.Id = LTD.TrustyDraftId  and TD.ActiveStatusId<>3
                                    join Bank.CurrencyType C on C.Id = TD.CustomerId and C.ActiveStatusId <> 3
                                    where L.ActiveStatusId <>3 and L.LetterNumber = @LetterNumber

";


        using var multi = await connection.QueryMultipleAsync(query, new { LetterNumber = letterNumber });
        response = multi.ReadAsync<GetReferalLetterQueryResult>().GetAwaiter().GetResult()?.FirstOrDefault();
        if (response is null) throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
        response.Borker = multi.ReadAsync<GetBroker>().GetAwaiter().GetResult().FirstOrDefault();
        response.TrustyDraftList = multi.ReadAsync<GetTrustyDraft>().GetAwaiter().GetResult().ToList();
        var branch = multi.ReadAsync<Application.Query.Contract.Features.TrustyDrafts.ReferalLetters.GetBranch>().GetAwaiter().GetResult().FirstOrDefault();
        var customer = multi.ReadAsync<Application.Query.Contract.Features.TrustyDrafts.ReferalLetters.GetCustomer>().GetAwaiter().GetResult().FirstOrDefault();
        var currencyType = multi.ReadAsync<Application.Query.Contract.Features.TrustyDrafts.ReferalLetters.GetDraftCurrencyType>().GetAwaiter().GetResult().FirstOrDefault();

        if (response.TrustyDraftList is not null)
        {
            foreach (var item in response.TrustyDraftList)
            {
                item.Branch = branch;
                item.Customer = customer;
                item.DraftCurrencyType = currencyType;

            }
        }


        response.NullCheck();
        return response;
    }
}
