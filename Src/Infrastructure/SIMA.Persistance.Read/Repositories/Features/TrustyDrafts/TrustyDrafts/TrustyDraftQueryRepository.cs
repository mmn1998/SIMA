using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryRequests;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts.RelationDataOnTrustyDraft;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Resources;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.TrustyDrafts;

public class TrustyDraftQueryRepository : ITrustyDraftQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    private readonly ISimaIdentity _simaIdentity;

    public TrustyDraftQueryRepository(IConfiguration configuration, ISimaIdentity simaIdentity)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
select  
    TD.Id TrustyDraftId,
    TD.DraftNumber,
    TD.DraftNumberBasedOnOrder,
	TD.BranchId,
	BR.Name BranchName,
	BR.Code BranchCode,
	TD.BrokerId,
	BRK.Name BrokerName,
	BRK.Code BrokerCode,
    TD.DraftIssueDate,
	td.IssueId DraftIssueId,
    i.Code DraftIssueCode,
	i.CurrenStepId DraftIssueCurrentStepId,
	s.Name DraftIssueCurrentStepName,
    TD.DraftRequestAmount,
    TD.DraftRequestAmountBasedOnUsd,
    TD.DraftNetAmount,
    TD.DraftRequestNetAmountBasedOnUsd,
    TD.PayingBankName,
    TD.InterMediateBankName IntermediateBankName,
	td.CreatedAt,
	(p.FirstName + ' ' + p.LastName) CreatedBy,
	TD.DraftCurrencyTypeId CurrencyTypeId,
	CT.Name CurrencyTypeName,
	CT.Code CurrencyTypeCode,
	TD.DraftStatusId,
	DS.Name DraftStatusName,
	DS.Code DraftStatusCode,
	TD.DraftValorStatusId,
	DVS.Name DraftValorStatusName,
	DVS.Code DraftValorStatusCode,
	WDM.Id WageDetuctionMethodId,
	WDM.Name WageDetuctionMethodName,
	WDM.Code WageDetuctionMethodCode,
	DT.Name DraftTypeName,
	DT.Id DraftTypeId,
	BT.Id BrokerTypeId,
	BT.Name BrokerTypeName,
	C.Id CustomerId,
	C.Name CustomerName,
	PT.Id PaymentTypeId,
	PT.Name PaymentTypeName,
    TD.ValorDate,
    TD.MainShareFromWage,
    TD.BuyShareFromWage,
    TD.BranchLetterNumber
from TrustyDraft.TrustyDraft TD
left join Bank.Branch BR on BR.Id = TD.BranchId and BR.ActiveStatusId<>3
left join Bank.Broker BRK on BRK.Id = TD.BrokerId and BRK.ActiveStatusId<>3
Inner join IssueManagement.Issue I on I.Id = td.IssueId  and I.ActiveStatusId <>3
Inner join Project.Step S on S.Id = I.CurrenStepId and S.ActiveStatusId <>3
inner join Authentication.Users U on U.Id = td.CreatedBy and U.ActiveStatusId<>3
inner join Authentication.Profile P On P.Id = U.ProfileID and p.ActiveStatusId<>3
left join TrustyDraft.DraftType DT on DT.Id = TD.DraftTypeId and DT.ActiveStatusId <> 3
left join Bank.CurrencyType CT on CT.Id = TD.DraftCurrencyTypeId and CT.ActiveStatusId<>3
left join TrustyDraft.DraftStatus DS on DS.Id = TD.DraftStatusId and DS.ActiveStatusId<>3
left join TrustyDraft.DraftValorStatus DVS on DVS.Id = TD.DraftValorStatusId and DVS.ActiveStatusId<>3
left join TrustyDraft.WageDeductionMethod WDM on WDM.Id = TD.WageDeductionMethodId and WDM.ActiveStatusId<>3
left join Bank.BrokerType BT on BT.Id = TD.BrokerTypeId and BT.ActiveStatusId <> 3
left join TrustyDraft.InquiryRequest IR on IR.Id = TD.InquiryRequestId and IR.ActiveStatusId <> 3
left join Bank.PaymentType PT on PT.Id = IR.PaymentTypeId and PT.ActiveStatusId <> 3
join Bank.Customer C on C.Id = TD.CustomerId and C.ActiveStatusId <> 3
where
  (isnull(dbo.FN_GetBranchIdByUserId(@UserId),0) = 0 OR dbo.FN_GetBranchIdByUserId(@UserId) = td.BranchId)
";
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<IEnumerable<GetAllTrustyDraftsQueryResult>>> GetAll(GetAllTrustyDraftsQuery request)
    {
        throw new NotImplementedException();
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
        var response = await multi.ReadAsync<GetAllTrustyDraftsQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<Result<IEnumerable<GetAllDraftForPaymentResult>>> GetAllDraftForPayment(GetAllDraftForPayment request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        string queryCount = $@" WITH Query as(
						                    {_mainQuery}
AND 
    (SELECT COUNT(*) FROM TrustyDraft.Reconsilation R WHERE R.TrustyDraftId = TD.Id AND R.ActiveStatusId <> 3) = 0
    AND 
    (SELECT COUNT(*) FROM TrustyDraft.ReferralLetter RL WHERE RL.TrustyDraftId = TD.Id AND RL.ActiveStatusId <> 3) > 0
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


        string query = $@" WITH Query as(
							                  {_mainQuery}
AND 
    (SELECT COUNT(*) FROM TrustyDraft.Reconsilation R WHERE R.TrustyDraftId = TD.Id AND R.ActiveStatusId <> 3) = 0
    AND 
    (SELECT COUNT(*) FROM TrustyDraft.ReferralLetter RL WHERE RL.TrustyDraftId = TD.Id AND RL.ActiveStatusId <> 3) > 0
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        dynaimcParameters.Item2.Add("UserId", _simaIdentity.UserId);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetAllDraftForPaymentResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<Result<IEnumerable<GetAllReconcilliationResult>>> GetAllReconcilliation(GetAllReconcilliation request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        string queryCount = $@" WITH Query as(
						                    {_mainQuery}
AND 
    (SELECT COUNT(*) FROM TrustyDraft.Reconsilation R WHERE R.TrustyDraftId = TD.Id AND R.ActiveStatusId <> 3) > 0
    AND 
    (SELECT COUNT(*) FROM TrustyDraft.ReferralLetter RL WHERE RL.TrustyDraftId = TD.Id AND RL.ActiveStatusId <> 3) > 0
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


        string query = $@" WITH Query as(
							                  {_mainQuery}
AND 
    (SELECT COUNT(*) FROM TrustyDraft.Reconsilation R WHERE R.TrustyDraftId = TD.Id AND R.ActiveStatusId <> 3) > 0
    AND 
    (SELECT COUNT(*) FROM TrustyDraft.ReferralLetter RL WHERE RL.TrustyDraftId = TD.Id AND RL.ActiveStatusId <> 3) > 0
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        dynaimcParameters.Item2.Add("UserId", _simaIdentity.UserId);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetAllReconcilliationResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<Result<IEnumerable<GetTrustyDraftReportQueryResult>>> GetReport(GetTrustyDraftReportQuery request)
    {
		    using var connection = new SqlConnection(_connectionString);
		    await connection.OpenAsync();

		    string query = $@"
		SELECT     td.*, B.Name AS BranchName, C.Name AS Customername, TrustyDraft.DraftOrigin.Name AS DraftOriginName, 
                         TrustyDraft.DraftType.Name AS DraftTypeName, Bank.Broker.Name AS BrokerNameb, Bank.CurrencyType.Name AS CurrencyTypeName, Bank.BranchType.Name AS BranchTypeName, 
                         TrustyDraft.DraftStatus.Name AS DraftStatusName
FROM            TrustyDraft.TrustyDraft AS TD left join 
                         Bank.Branch AS B ON TD.BranchId = B.Id left join 
                         Bank.Customer AS C ON TD.CustomerId = C.Id left join 
                         TrustyDraft.DraftOrigin ON TD.DraftOriginId = TrustyDraft.DraftOrigin.Id left join 
                         TrustyDraft.DraftType ON TD.DraftTypeId = TrustyDraft.DraftType.Id left join 
                         Bank.Broker ON TD.BrokerId = Bank.Broker.Id left join 
                         Bank.CurrencyType ON TD.CancellationCurrencyTypeId = Bank.CurrencyType.Id left join 
                         Bank.BranchType ON B.BranchTypeId = Bank.BranchType.Id LEFT join 
                         TrustyDraft.DraftStatus ON TD.DraftStatusId = TrustyDraft.DraftStatus.Id 
WHERE        (TD.ActiveStatusId = 4)
AND (@FromDate IS NULL OR TD.CreatedAt >= @FromDate)
AND (@ToDate IS NULL OR TD.CreatedAt <= @ToDate)
AND (@DraftyNumber IS NULL OR TD.DraftNumber = @DraftyNumber)
AND (@CreatedBy IS NULL OR TD.CreatedBy = @CreatedBy) 
";
		    var dynaimcParameters = (query).GenerateQuery(request);
		    dynaimcParameters.Item2.Add("UserId", _simaIdentity.UserId);
		    using var multi = await connection.QueryMultipleAsync(query, new { FromDate = request.FromDateMiladi,ToDate = request.ToDateMiladi, DraftyNumber = request.DraftyNumber,CreatedBy = request.CreatedBy });
		    var response = await multi.ReadAsync<GetTrustyDraftReportQueryResult>();
		    return Result.Ok(response, request);
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> GetAllRequested(GetAllTrustyDraftRequested request)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string queryCount = $@" WITH Query as(
						                    {_mainQuery}
     AND 
    (SELECT COUNT(*) FROM TrustyDraft.Reconsilation R WHERE R.TrustyDraftId = TD.Id AND R.ActiveStatusId <> 3) = 0
    AND 
    (SELECT COUNT(*) FROM TrustyDraft.ReferralLetter RL WHERE RL.TrustyDraftId = TD.Id AND RL.ActiveStatusId <> 3) = 0
    And   s.Id != 7275625281
           
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


            string query = $@" WITH Query as(
							                  {_mainQuery}
     AND 
    (SELECT COUNT(*) FROM TrustyDraft.Reconsilation R WHERE R.TrustyDraftId = TD.Id AND R.ActiveStatusId <> 3) = 0
    AND 
    (SELECT COUNT(*) FROM TrustyDraft.ReferralLetter RL WHERE RL.TrustyDraftId = TD.Id AND RL.ActiveStatusId <> 3) = 0
        And   s.Id != 7275625281  
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";

            /// s.Id != 7275625281    برای این شرط طبقه گفته تیم تحلیل فقط داده هایی در ارجاع به کارگزاری نمایش داده می شود که گام فرآیند ان ارجاع به کارگزاری نباشد


            var dynaimcParameters = (queryCount + query).GenerateQuery(request);
            dynaimcParameters.Item2.Add("UserId", _simaIdentity.UserId);
            using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
            var count = await multi.ReadFirstAsync<int>();
            var response = await multi.ReadAsync<GetAllTrustyDraftRequestedResult>();
            return Result.Ok(response, request, count);
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    public async Task<GetTrustyDraftQueryResult> GetById(long id)
    {
        try
        {
            var response = new GetTrustyDraftQueryResult();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string query = @"
                          select
                              TD.Id TrustyDraftId,
                              TD.DraftNumber,
                              TD.DraftNumberBasedOnOrder,  
                              TD.DraftIssueDate,
		                      td.IssueId DraftIssueId,
		                      TD.BlockingNumber,
                              TD.DraftRequestAmount,
                              TD.DraftRequestAmountBasedOnUsd,
                              TD.DraftNetAmount,
		                      TD.DraftNetAmountBasedOnUsd,
		                      TD.DraftNetAmountBasedOnEur,
		                      TD.DraftRequestAmountBasedOnEur,
                              TD.DraftRequestNetAmountBasedOnUsd,
                              TD.DraftNetAmountBasedOnUsd,
                              TD.DraftNetAmountBasedOnEur,
                              TD.PayingBankName,
                              TD.BrokerBankName,
                              TD.InterMediateBankName IntermediateBankName,
		                      TD.DetailBic,
                              TD.DraftAcceptDate,
                              TD.DraftAcceptTime,
		                      TD.OriginAmount,
		                      TD.OrderingExternalAccountNumber,
		                      TD.BeneficiaryExternalAccountNumber,
		                      td.CreatedAt,
		                      (p.FirstName + ' ' + p.LastName) CreatedBy,
		                      TD.BeneficiaryAccountNumber,
		                      TD.BeneficiaryAddress,
		                      TD.BeneficiaryPhoneNumber,
		                      TD.BeneficiaryName,
		                      TD.BeneficiaryIban,
		                      TD.IssueReason,
		                      RL.Id ReferralLetterId,
		                      rl.LetterNumber ReferralLetterNumber,
		                      rl.LetterDate ReferralLetterDate,
		                      RL.LetterDocumentId,
		                      rl.CreatedAt ReferralLetterCreatedAt,
		                      (p1.FirstName + ' ' + p1.LastName) ReferralLetterCreatedBy,
		                      TD.ValorDate,
		                      TD.MainShareFromWage,
		                      TD.BuyShareFromWage,
		                      TD.BranchLetterNumber
                      from TrustyDraft.TrustyDraft TD
                      left join Bank.Broker BRK on BRK.Id = TD.BrokerId and BRK.ActiveStatusId<>3
                    inner join Authentication.Users U on U.Id = td.CreatedBy and U.ActiveStatusId<>3
                    inner join Authentication.Profile P On P.Id = U.ProfileID and p.ActiveStatusId<>3
                    left join TrustyDraft.ReferralLetter RL on RL.TrustyDraftId = TD.Id and RL.ActiveStatusId<>3
                    left join Authentication.Users U1 on U1.Id = RL.CreatedBy and U.ActiveStatusId<>3
                    left join Authentication.Profile P1 On P1.Id = U1.ProfileID and p.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3


                    ------- branchInfo
                    select
		                      TD.BranchId,
		                      BR.Name BranchName,
		                      BR.Code BranchCode
                    from TrustyDraft.TrustyDraft TD
                      left join Bank.Branch BR on BR.Id = TD.BranchId and BR.ActiveStatusId<>3
                      where TD.Id = @Id And TD.ActiveStatusId <> 3
                      ------- brokerInfo
                      select
		                      TD.BrokerId,
		                      BRK.Name BrokerName,
		                      BRK.Code BrokerCode
                      from TrustyDraft.TrustyDraft TD
                      left join Bank.Broker BRK on BRK.Id = TD.BrokerId and BRK.ActiveStatusId<>3
                      where TD.Id = @Id And TD.ActiveStatusId <> 3
                      ------- customerInfo
                      select
		                      td.CustomerId,
		                      C.Name CustomerName,
		                      c.CustomerNumber
                       from TrustyDraft.TrustyDraft td
                    left join Bank.Customer C on c.Id = td.CustomerId and c.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3

                      ------- draftOriginInfo
                    select		  
		                      td.DraftOriginId,
		                      DO.Name DraftOriginName
                    from TrustyDraft.TrustyDraft TD
                    left join TrustyDraft.DraftOrigin DO on DO.Id = TD.DraftOriginId and DO.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3
                      ------- DraftStatusInfo
                      select 
		                      TD.DraftStatusId,
		                      DS.Name DraftStatusName,
		                      DS.Code DraftStatusCode
                      from TrustyDraft.TrustyDraft TD
                      left join TrustyDraft.DraftStatus DS on DS.Id = TD.DraftStatusId and DS.ActiveStatusId<>3
                      where TD.Id = @Id And TD.ActiveStatusId <> 3
                      ------- DraftValorStatusInfo
                      select 
		                      TD.DraftValorStatusId,
		                      DVS.Name DraftValorStatusName,
		                      DVS.Code DraftValorStatusCode
                      from TrustyDraft.TrustyDraft TD
                      left join TrustyDraft.DraftValorStatus DVS on DVS.Id = TD.DraftValorStatusId and DVS.ActiveStatusId<>3
                      where TD.Id = @Id And TD.ActiveStatusId <> 3
                      ------- CurrencyTypeInfo
                      select 
		                      TD.DraftCurrencyTypeId CurrencyTypeId,
		                      CT.Name CurrencyTypeName,
		                      CT.Code CurrencyTypeCode
                      from TrustyDraft.TrustyDraft TD
                      left join Bank.CurrencyType CT on CT.Id = TD.DraftCurrencyTypeId and CT.ActiveStatusId<>3
                      where TD.Id = @Id And TD.ActiveStatusId <> 3


                    -------- TrustyDraftDocument
                    select
                    BCSD.Id,
                    D.Id DocumentId,
                    d.Name DocumentFileName,
                    d.DocumentTypeId,
                    dt.Name DocumentTypeName,
                    d.FileExtensionId,
                    de.Name DocumentExtensionName,
                    s.Name AttachStepName,
                    BCSD.CreatedAt,
                    P.FirstName + ' ' + P.LastName CreatedBy
                      from TrustyDraft.TrustyDraft TD
                    inner join TrustyDraft.TrustyDraftDocument BCSD on BCSD.TrustyDraftId = TD.Id and BCSD.ActiveStatusId<>3
                    inner join DMS.Documents D on BCSD.DocumentId = D.Id and D.ActiveStatusId<>3
                    inner join DMS.DocumentType DT on DT.Id = D.DocumentTypeId and DT.ActiveStatusId<>3
                    inner join DMS.DocumentExtension DE on DE.Id = D.FileExtensionId and DE.ActiveStatusId<>3
                    left join Project.Step S on S.Id = D.AttachStepId and s.ActiveStatusID<>3
                    left join Authentication.Users U on U.Id = D.CreatedBy 
                    left join Authentication.Profile P on P.Id = U.ProfileID
                    where TD.Id = @Id
                    order by D.CreatedAt desc


                    -------- PaymentReceiptInfoList
                    select
                          PRI.Id,
                          PRI.PaymentDate,
                          PRI.PaymentAmount,
                          PRI.TrustyDraftDocumentId,
		                  TDD.DocumentId,
		                  CT.Name CurrencyTypeName ,
		                  CT.Id CurrencyTypeId,
		                  PT.Id PaymentTypeId ,
		                  PT.Name PaymentTypeName,
                          PRI.CreatedAt,
                          (p.FirstName + ' ' + p.LastName) CreatedBy
                   from TrustyDraft.TrustyDraft TD
                   inner join TrustyDraft.PaymentReceiptInfo PRI on PRI.TrustyDraftId = TD.Id
                   left join Bank.PaymentType PT on PT.Id = TD.PaymentTypeId and PT.ActiveStatusId <>3 
                   left join Bank.CurrencyType CT on CT.Id = TD.DraftCurrencyTypeId and CT.ActiveStatusId <>3 
                   inner join Authentication.Users U on U.Id = PRI.CreatedBy and U.ActiveStatusId<>3
                   inner join Authentication.Profile P On P.Id = U.ProfileID and p.ActiveStatusId<>3
                   left join TrustyDraft.TrustyDraftDocument TDD on TDD.Id = PRI.TrustyDraftDocumentId 
                    where TD.Id = @Id
                    -------- StatementList
                    select
                             s.Id,
		                     s.StatementDate,
		                     s.Description,
		                     s.TrustyDocumentId,
		                     s.CreatedAt,
		                     (p.FirstName + ' ' + p.LastName) CreatedBy
                      from TrustyDraft.TrustyDraft TD
                      inner join TrustyDraft.Statement S on S.TrustyDraftId = TD.Id and s.ActiveStatusId<>3
                      inner join Authentication.Users U on U.Id = S.CreatedBy and U.ActiveStatusId<>3
                    inner join Authentication.Profile P On P.Id = U.ProfileID and p.ActiveStatusId<>3
                    where TD.Id = @Id
                    -------- ReconciliationList
                    select
                             R.Id,
		                     R.ReconsilationTypeId,
		                     R.Description,
		                     R.IsInformedByBranch,
		                     r.InformedDate,
		                     R.CreatedAt,
		                     (p.FirstName + ' ' + p.LastName) CreatedBy
                      from TrustyDraft.TrustyDraft TD
                      inner join TrustyDraft.Reconsilation R on R.TrustyDraftId = TD.Id and R.ActiveStatusId<>3
                      inner join TrustyDraft.ReconsilationType RT on RT.Id = R.ReconsilationTypeId and rt.ActiveStatusId<>3
                      inner join Authentication.Users U on U.Id = R.CreatedBy and U.ActiveStatusId<>3
                    inner join Authentication.Profile P On P.Id = U.ProfileID and p.ActiveStatusId<>3
                    where TD.Id = @Id
                    -------- BrokerAddressList
                    select
                              BAB.AddressTypeId,
		                      BAB.Id BrokerAddressListId,
		                      BAB.Address,
		                      BAB.PostalCode,
		                      a.Name AddressTypeName
                    from TrustyDraft.TrustyDraft TD
                    inner join Bank.Broker BRK on BRK.Id = TD.BrokerId and BRK.ActiveStatusId<>3
                    inner join Bank.BrokerAddressBook BAB on BAB.BrokerId = BRK.Id and BAB.ActiveStatusId<>3
                    inner join Basic.AddressType A on A.Id = BAB.AddressTypeId and a.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3
                    -------- BrokerPhoneList
                    select
		                      BAB.Id BrokerPhoneListId,
		                      BAB.PhoneNumber,
		                      BAB.PhoneTypeId,
		                      a.Name PhoneTypeName
                    from TrustyDraft.TrustyDraft TD
                    inner join Bank.Broker BRK on BRK.Id = TD.BrokerId and BRK.ActiveStatusId<>3
                    inner join Bank.BrokerPhoneBook BAB on BAB.BrokerId = BRK.Id and BAB.ActiveStatusId<>3
                    inner join Basic.PhonType A on A.Id = BAB.PhoneTypeId and a.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3
                    -------- BrokerAccountList
                    select
		                      BAB.Id BrokerAccountListId,
		                      BAB.IBANNumber
                    from TrustyDraft.TrustyDraft TD
                    inner join Bank.Broker BRK on BRK.Id = TD.BrokerId and BRK.ActiveStatusId<>3
                    inner join Bank.BrokerAccountBook BAB on BAB.BrokerId = BRK.Id and BAB.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3



                    ------- BrokerSecondLevelAddress
                    select 
                    BSLA.Id,
                    BSLA.Name,
                    BSLA.PhoneNumber,
                    BSLA.Address
                    from TrustyDraft.TrustyDraft Td
                    inner join TrustyDraft.BrokerSecondLevelAddressBook BSLA on BSLA.Id = Td.BrokerSecondLevelAddressBookId and BSLA.ActiveStatusId<>3
                    where Td.Id = @Id
                    ------- WageDeductionMethod
                    select 
                    WDM.Id WageDeductionMethodId,
                    WDM.Name WageDeductionMethodName,
                    WDM.Code WageDeductionMethodCode
                    from TrustyDraft.TrustyDraft Td
                    inner join TrustyDraft.WageDeductionMethod WDM on WDM.Id = Td.WageDeductionMethodId and WDM.ActiveStatusId<>3
                    where Td.Id = @Id


                    ------- WageShareStatus
                    select 
                    WS.Id WageShareStatusId,
                    WS.Name WageShareStatusName,
                    WS.Code WageShareStatusCode
                    from TrustyDraft.TrustyDraft Td
                    inner join TrustyDraft.AgentBankWageShareStatus WS on WS.Id = Td.AgentBankWageShareStatusId and WS.ActiveStatusId<>3
                    where Td.Id = @Id

                    ------ DraftType

                    select 
                    DT.Id DraftTypeId,
                    DT.Name DraftTypeName,
                    DT.Code DraftTypeCode
                    from TrustyDraft.TrustyDraft Td
                    inner join TrustyDraft.DraftType DT on DT.Id = Td.DraftTypeId and DT.ActiveStatusId<>3
                    where Td.Id = @Id


                    ------ PaymentType

                    select 
                        DT.Id,
                        DT.Name,
                        DT.Code
                        from TrustyDraft.TrustyDraft Td
                        left join TrustyDraft.InquiryRequest IR on IR.Id = TD.InquiryRequestId and IR.ActiveStatusId <> 3
                        inner join Bank.PaymentType DT on DT.Id = IR.PaymentTypeId and DT.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3


                ------- AccountTypeInfo
                    select
                              TD.AccountTypeId,
                              A.Name AccountTypeName,
                              A.Code AccountTypeCode
                    from TrustyDraft.TrustyDraft td
                    join Bank.AccountType A on A.Id = td.AccountTypeId and A.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3


                         --------- BrokerType
 
                         select
                                   TD.BrokerTypeId,
                                   BT.Name BrokerTypeName,
                                   BT.Code BrokerTypeCode
                         from TrustyDraft.TrustyDraft TD
                         join Bank.BrokerType BT on BT.Id = TD.BrokerTypeId and BT.ActiveStatusId<>3
                         where TD.Id = @Id And TD.ActiveStatusId <> 3

                ------- ResponsibilityWageTypeInfo

                    select
                              TD.ResponsibilityWageTypeId,
                              RW.Name ResponsibilityWageTypeName,
                              RW.Code ResponsibilityWageTypeCode
                    from TrustyDraft.TrustyDraft td
                    join  TrustyDraft.ResponsibilityWageType RW  on RW.Id = td.ResponsibilityWageTypeId and RW.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3

                    --------- inquiryInfo
                    select 
                        IR.Id InquiryRequestId,
                        IR.BeneficiaryName,
                        IR.ReferenceNumber,
                        IR.DraftOrderNumber,
                        IR.ProformaNumber,
                        IR.Description RequestDescription,
	                    IR.BranchId,
	                    Br.Name BranchName,
                        Br.Code BranchCode,
                        C.Name CustomerName,
                        C.Id CustomerId,
                        PT.Name PaymentTypeName,
                        PT.Id PaymentTypeId,
                        IR.CreatedAt RequestCreatedAt,
                        P.FirstName + ' ' + P.LastName RequestCreatedBy,
	                    IRes.Id InquiryResponseId,
	                    IRes.BrokerInquiryStatusId,
	                    BIS.Name BrokerInquiryStatusName,
	                    IRes.BrokerId,
	                    B2.Name BrokerName,
	                    IRes.WageRateId,
	                    WR.Name WageRateName,
	                    IRes.CalculatedWage,
	                    IRes.ExcessWage,
                        TD.DraftNumber,
	                    IRes.CreatedAt ResponseCreatedAt,
	                    P2.FirstName + ' ' + P2.LastName ResponseCreatedBy,
                        IR.CreatedAt,
                        IRes.ValidityPeriod,
	                    IRes.Description ResponseDescription,
                        IRC.Amount,
	                    IRC.CurrencyTypeId,
	                    CT.Name CurrencyTypeName,
                        IR.DraftOriginId,
	                    DO.Name DraftOriginName,
	                    IR.DraftOrderDate,
	                    IR.ProformaDate,
                        IR.ProformaCurrencyTypeId,
                        CT2.Name ProformaCurrencyTypeName,
	                    IR.ProformaAmount,
                        TD.DraftNumber
                    from TrustyDraft.InquiryRequest IR 
                    Inner Join TrustyDraft.TrustyDraft TD on Td.InquiryRequestId = IR.Id
                    join Bank.Customer C on C.Id = IR.CustomerId and C.ActiveStatusId<>3
                    join Bank.PaymentType PT on PT.Id = IR.PaymentTypeId and PT.ActiveStatusId<>3
                    join Authentication.Users U on U.Id = IR.CreatedBy  and U.ActiveStatusId <> 3
                    join Authentication.Profile P on P.Id = U.ProfileID
                    join Bank.Branch Br on Br.Id = IR.BranchId and Br.ActiveStatusId<>3
                    LEFT JOIN TrustyDraft.InquiryResponse IRes on IRes.InquiryRequestId = IR.Id and IRes.ActiveStatusId<>3
                    Left join TrustyDraft.BrokerInquiryStatus BIS on BIS.Id = IRes.BrokerInquiryStatusId and BIS.ActiveStatusId<>3
                    left join Bank.Broker B2 on B2.Id = IRes.BrokerId and B2.ActiveStatusId <> 3
                    left join TrustyDraft.WageRate WR on WR.Id = IRes.WageRateId and WR.ActiveStatusId <> 3
                    Left join Authentication.Users U2 on U2.Id = IRes.CreatedBy  and U2.ActiveStatusId <> 3
                    Left join Authentication.Profile P2 on P2.Id = U2.ProfileID
                    LEFT JOIN TrustyDraft.InquiryRequestCurrency IRC on IRC.Id = IRes.InquiryRequestCurrencyId and IR.ActiveStatusId<>3 and IRC.ActiveStatusId<>3
                    LEFT JOIN Bank.CurrencyType CT on CT.Id = IRC.CurrencyTypeId and CT.ActiveStatusId<>3
                    LEFT JOIN Bank.CurrencyType CT2 on CT2.Id = IR.ProformaCurrencyTypeId and CT2.ActiveStatusId<>3
                    LEFT JOIN TrustyDraft.DraftOrigin DO on DO.Id = IR.DraftOriginId and DO.ActiveStatusId<>3
                    where IR.ActiveStatusId <> 3 and Td.Id = @Id


                    ------inquiry documents

                    select
                    BCSD.Id,
                    d.Name DocumentFileName,
                    D.Id DocumentId,
                    d.DocumentTypeId,
                    dt.Name DocumentTypeName,
                    d.FileExtensionId,
                    de.Name DocumentExtensionName,
                    s.Name AttachStepName,
                    BCSD.CreatedAt,
                    P.FirstName + ' ' + P.LastName CreatedBy
                      from TrustyDraft.TrustyDraft TD
                     inner Join TrustyDraft.InquiryRequest IR on TD.InquiryRequestId = IR.Id
                    inner join TrustyDraft.InquiryRequestDocument BCSD on BCSD.InquiryRequestId = IR.Id and BCSD.ActiveStatusId<>3
                    inner join DMS.Documents D on BCSD.DocumentId = D.Id and D.ActiveStatusId<>3
                    inner join DMS.DocumentType DT on DT.Id = D.DocumentTypeId and DT.ActiveStatusId<>3
                    inner join DMS.DocumentExtension DE on DE.Id = D.FileExtensionId and DE.ActiveStatusId<>3
                    left join Project.Step S on S.Id = D.AttachStepId and s.ActiveStatusID<>3
                    left join Authentication.Users U on U.Id = D.CreatedBy 
                    left join Authentication.Profile P on P.Id = U.ProfileID
                    where TD.Id = @Id
                    order by D.CreatedAt desc

                    -------- inquiryRequestCurrency
                    select 
                    CT.Id CurrencyTypeId,
                    CT.Name CurrencyTypeName,
                    IRC.Amount
                    from TrustyDraft.InquiryRequest IR
                    inner join TrustyDraft.TrustyDraft TD on TD.InquiryRequestId = IR.Id and IR.ActiveStatusId<>3
                    inner join TrustyDraft.InquiryRequestCurrency IRC on IR.Id = IRC.InquiryRequestId and IRC.ActiveStatusId<>3
                    inner join Bank.CurrencyType CT on CT.Id = IRC.CurrencyTypeId and CT.ActiveStatusId<>3
                    where TD.ActiveStatusId<>3 AND TD.Id = @Id
";


            using var multi = await connection.QueryMultipleAsync(query, new { Id = id });
            response = multi.ReadAsync<GetTrustyDraftQueryResult>().GetAwaiter().GetResult()?.FirstOrDefault();
            if (response is null) throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
            response.BranchInfo = await multi.ReadFirstOrDefaultAsync<GetBranchQueryResult>();
            response.BrokerInfo = await multi.ReadFirstOrDefaultAsync<GetBrokerQueryResult>();
            response.CustomerInfo = await multi.ReadFirstOrDefaultAsync<GetCustomerQueryResult>();
            response.DraftOriginInfo = await multi.ReadFirstOrDefaultAsync<GetDraftOriginQueryResult>();
            response.DraftStatusInfo = await multi.ReadFirstOrDefaultAsync<GetDraftStatusQueryResult>();
            response.DraftValorStatusInfo = await multi.ReadFirstOrDefaultAsync<GetDraftValorStatusQueryResult>();
            response.CurrencyTypeInfo = await multi.ReadFirstOrDefaultAsync<GetCurrencyTypeQueryResult>();
            response.TrustyDraftDocumentList = await multi.ReadAsync<GetTrustyDraftDocumentResult>();
            response.PaymentReceiptInfoList = await multi.ReadAsync<GetPaymentReceiptInfoResult>();
            response.StatementList = await multi.ReadAsync<GetStatementResult>();
            response.ReconciliationList = await multi.ReadAsync<GetReconciliationResult>();
            response.BrokerAddressList = await multi.ReadAsync<GetBrokerAddressResult>();
            response.BrokerPhoneList = await multi.ReadAsync<GetBrokerPhoneResult>();
            response.BrokerAccountList = await multi.ReadAsync<GetBrokerAccountResult>();
            response.BorkerSecondLevelAddressInfo = await multi.ReadFirstOrDefaultAsync<GetBrokerSecondLevelAddressQueryResult>();
            response.WageDeductionMethodInfo = await multi.ReadFirstOrDefaultAsync<GetWageDeductionMethodQueryResult>();
            response.AgentBankWageShareStatusInfo = await multi.ReadFirstOrDefaultAsync<GetAgentBankWageShareStatusResult>();
            response.DraftTypeInfo = await multi.ReadFirstOrDefaultAsync<GetDraftTypeResult>();
            response.PaymentTypeInfo = await multi.ReadFirstOrDefaultAsync<GetPaymentTypeResult>();
            response.AccountTypeInfo = await multi.ReadFirstOrDefaultAsync<GetAccountTypeResult>();
            response.BrokerTypeInfo = await multi.ReadFirstOrDefaultAsync<GetBrokerTypeResult>();
            response.ResponsibilityWageTypeInfo = await multi.ReadFirstOrDefaultAsync<GetResponsibilityWageTypeResult>();
            response.InquiryInfo = await multi.ReadFirstOrDefaultAsync<GetInquiryRequestQueryResult>();
            // this query should come after all of other queries
            if (response.InquiryInfo is not null)
            {
                response.InquiryInfo.InquiryRquestDocumentList = await multi.ReadAsync<GetInquiryRequestDocumentQueryResult>();
                response.InquiryInfo.InquiryRquestCurrencyList = await multi.ReadAsync<GetInquiryRequestCurrencyQueryResult>();
            }


            response.NullCheck();
            return response;

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<GetTrustyDraftInqueryQueryResult> GetByIdForInquery(long id)
    {
        try
        {
            var response = new GetTrustyDraftInqueryQueryResult();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string query = @"
                      select
                              TD.Id TrustyDraftId,
                              TD.DraftNumber,
                              TD.DraftNumberBasedOnOrder,  
                              TD.DraftIssueDate,
                              td.IssueId DraftIssueId,
                              TD.BlockingNumber,
                              TD.DraftRequestAmount,
                              TD.DraftNetAmount,
                              TD.DraftNetAmountBasedOnUsd,
                              TD.DraftNetAmountBasedOnEur,
                              TD.DraftRequestAmountBasedOnUsd,
                              TD.DraftRequestAmountBasedOnEur,
                              TD.DraftRequestNetAmountBasedOnUsd,
                              TD.DraftNetAmountBasedOnUsd,
                              TD.DraftNetAmountBasedOnEur,
                              TD.PayingBankName,
                              TD.InterMediateBankName IntermediateBankName,
                              TD.DetailBic,
                              TD.OriginAmount,
                              TD.OrderingExternalAccountNumber,
                              TD.BeneficiaryExternalAccountNumber,
                              td.CreatedAt,
                              (p.FirstName + ' ' + p.LastName) CreatedBy,
                              TD.BeneficiaryName,
                              TD.BeneficiaryAccountNumber,
                              TD.BeneficiaryAddress,
                              TD.BeneficiaryPhoneNumber,
                              TD.BeneficiaryIban,
                              rl.LetterNumber,
                              rl.LetterDate,
                              RL.LetterDocumentId,
                              TD.BrokerBankName,
                              TD.AgentBank,
                              TD.IssueReason,
                              TD.DraftAcceptDate,
                              TD.DraftAcceptTime,
                              TD.ValorDate,
                              rl.CreatedAt ReferralLetterCreatedAt,
                              (p1.FirstName + ' ' + p1.LastName) ReferralLetterCreatedBy
                      from TrustyDraft.TrustyDraft TD
                      left join Bank.Broker BRK on BRK.Id = TD.BrokerId and BRK.ActiveStatusId<>3
                    inner join Authentication.Users U on U.Id = td.CreatedBy and U.ActiveStatusId<>3
                    inner join Authentication.Profile P On P.Id = U.ProfileID and p.ActiveStatusId<>3
                    left join TrustyDraft.ReferralLetterDraftList RLL on RLL.TrustyDraftId = td.Id and RLL.ActiveStatusId<>3
                    left join TrustyDraft.ReferralLetter RL on RLL.ReferralLetterId = RL.Id and RL.ActiveStatusId<>3
                    left join Authentication.Users U1 on U1.Id = RL.CreatedBy and U.ActiveStatusId<>3
                    left join Authentication.Profile P1 On P1.Id = U1.ProfileID and p.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3


                    ------- branchInfo
                    select
                              TD.BranchId,
                              BR.Name BranchName,
                              BR.Code BranchCode
                    from TrustyDraft.TrustyDraft TD
                    join Bank.Branch BR on BR.Id = TD.BranchId and BR.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3


                    ------- customerInfo
                    select
                              td.CustomerId,
                              C.Name CustomerName,
                              c.CustomerNumber
                    from TrustyDraft.TrustyDraft td
                    join Bank.Customer C on c.Id = td.CustomerId and c.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3

                    ------- draftOriginInfo
                    select		  
                              td.DraftOriginId,
                              DO.Name DraftOriginName
                    from TrustyDraft.TrustyDraft TD
                    join TrustyDraft.DraftOrigin DO on DO.Id = TD.DraftOriginId and DO.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3


                      ------- DraftStatusInfo
                    select 
                          TD.DraftStatusId,
                          DS.Name DraftStatusName,
                          DS.Code DraftStatusCode
                    from TrustyDraft.TrustyDraft TD
                    join TrustyDraft.DraftStatus DS on DS.Id = TD.DraftStatusId and DS.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3

                      ------- DraftValorStatusInfo
                    select 
                              TD.DraftValorStatusId,
                              DVS.Name DraftValorStatusName,
                              DVS.Code DraftValorStatusCode
                    from TrustyDraft.TrustyDraft TD
                    join TrustyDraft.DraftValorStatus DVS on DVS.Id = TD.DraftValorStatusId and DVS.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3

                      ------- CurrencyTypeInfo
                    select 
                              TD.DraftCurrencyTypeId CurrencyTypeId,
                              CT.Name CurrencyTypeName,
                              CT.Code CurrencyTypeCode
                    from TrustyDraft.TrustyDraft TD
                    join Bank.CurrencyType CT on CT.Id = TD.DraftCurrencyTypeId and CT.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3


                    ------- AccountTypeInfo
                    select
                              TD.AccountTypeId,
                              A.Name AccountTypeName,
                              A.Code AccountTypeCode
                    from TrustyDraft.TrustyDraft td
                    join Bank.AccountType A on A.Id = td.AccountTypeId and A.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3



                    ------- ResponsibilityWageTypeInfo
                    select
                              TD.ResponsibilityWageTypeId,
                              RW.Name ResponsibilityWageTypeName,
                              RW.Code ResponsibilityWageTypeCode
                    from TrustyDraft.TrustyDraft td
                    join  TrustyDraft.ResponsibilityWageType RW  on RW.Id = td.ResponsibilityWageTypeId and RW.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3

                    ------- DraftReviewResultInfo
                    select
                              TD.DraftReviewResultId,
                              DR.Name DraftReviewResultName,
                              DR.Code DraftReviewResultCode
                    from TrustyDraft.TrustyDraft td
                    join  TrustyDraft.DraftReviewResult DR  on DR.Id = td.DraftReviewResultId and DR.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3


                    ------- CancellationResaonInfo
                    select
                              TD.CancellationResaonId,
                              CR.Name CancellationResaonName,
                              CR.Code CancellationResaonCode
                    from TrustyDraft.TrustyDraft td
                    join  TrustyDraft.CancellationResaon CR  on CR.Id = TD.CancellationResaonId and CR.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3
                    

                    ------- loanTypeInfo
                    select
                              TD.LoanTypeId,
                              LT.Name LoanTypeName,
                              LT.Code LoanTypeCode
                    from TrustyDraft.TrustyDraft td
                    join  Bank.LoanType LT on LT.Id = TD.LoanTypeId and LT.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3


                    ------- DraftTypeInfo
                    select
                              TD.DraftTypeId,
                              DT.Name DraftTypeName,
                              DT.Code DraftTypeCode
                    from TrustyDraft.TrustyDraft td
                    join  TrustyDraft.DraftType DT on DT.Id = TD.DraftTypeId and DT.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3

                    ------- DraftDestinationInfo
                    select
                              TD.DraftDestinationId,
                              DD.Name DraftDestinationName,
                              DD.Code DraftDestinationCode
                    from TrustyDraft.TrustyDraft td
                    join  TrustyDraft.DraftDestination DD on DD.Id = TD.DraftDestinationId and DD.ActiveStatusId<>3
                    where TD.Id = @Id And TD.ActiveStatusId <> 3

                         --------- BrokerType
 
                         select
                                   TD.BrokerTypeId,
                                   BT.Name BrokerTypeName,
                                   BT.Code BrokerTypeCode
                         from TrustyDraft.TrustyDraft TD
                         join Bank.BrokerType BT on BT.Id = TD.BrokerTypeId and BT.ActiveStatusId<>3
                         where TD.Id = @Id And TD.ActiveStatusId <> 3
";


            using var multi = await connection.QueryMultipleAsync(query, new { Id = id });
            response = multi.ReadAsync<GetTrustyDraftInqueryQueryResult>().GetAwaiter().GetResult()?.FirstOrDefault();
            if (response is null) throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
            response.BranchInfo = await multi.ReadFirstOrDefaultAsync<GetBranchQueryResult>();
            response.CustomerInfo = await multi.ReadFirstOrDefaultAsync<GetCustomerQueryResult>();
            response.DraftOriginInfo = await multi.ReadFirstOrDefaultAsync<GetDraftOriginQueryResult>();
            response.DraftStatusInfo = await multi.ReadFirstOrDefaultAsync<GetDraftStatusQueryResult>();
            response.DraftValorStatusInfo = await multi.ReadFirstOrDefaultAsync<GetDraftValorStatusQueryResult>();
            response.CurrencyTypeInfo = await multi.ReadFirstOrDefaultAsync<GetCurrencyTypeQueryResult>();
            response.AccountTypeInfo = await multi.ReadFirstOrDefaultAsync<GetAccountTypeResult>();
            response.ResponsibilityWageTypeInfo = await multi.ReadFirstOrDefaultAsync<GetResponsibilityWageTypeResult>();
            response.DraftReviewResultInfo = await multi.ReadFirstOrDefaultAsync<GetDraftReviewResult>();
            response.CancellationResaonInfo = await multi.ReadFirstOrDefaultAsync<GetCancellationResaonResult>();
            response.LoanTypeInfo = await multi.ReadFirstOrDefaultAsync<GetLoanTypeResult>();
            response.DraftTypeInfo = await multi.ReadFirstOrDefaultAsync<GetDraftTypeResult>();
            response.DraftDestinationInfo = await multi.ReadFirstOrDefaultAsync<GetDraftDestinationResult>();
            response.BrokerTypeInfo = await multi.ReadFirstOrDefaultAsync<GetBrokerTypeResult>();


            response.NullCheck();
            return response;

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> GetAllByBrokerId(GetAllTrustyDraftByBrokerQuery request)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();


            string query = $@"
							                  select  
                TD.Id TrustyDraftId,
                TD.DraftNumber,
                TD.DraftNumberBasedOnOrder,
	            TD.BranchId,
	            BR.Name BranchName,
	            BR.Code BranchCode,
	            TD.BrokerId,
	            BRK.Name BrokerName,
	            BRK.Code BrokerCode,
                TD.DraftIssueDate,
	            td.IssueId DraftIssueId,
                i.Code DraftIssueCode,
	            i.CurrenStepId DraftIssueCurrentStepId,
	            s.Name DraftIssueCurrentStepName,
                TD.DraftRequestAmount,
                TD.DraftRequestAmountBasedOnUsd,
                TD.DraftNetAmount,
                TD.DraftRequestNetAmountBasedOnUsd,
                TD.PayingBankName,
                TD.InterMediateBankName IntermediateBankName,
	            td.CreatedAt,
	            (p.FirstName + ' ' + p.LastName) CreatedBy,
	            TD.DraftCurrencyTypeId CurrencyTypeId,
	            CT.Name CurrencyTypeName,
	            CT.Code CurrencyTypeCode,
	            TD.DraftStatusId,
	            DS.Name DraftStatusName,
	            DS.Code DraftStatusCode,
	            TD.DraftValorStatusId,
	            DVS.Name DraftValorStatusName,
	            DVS.Code DraftValorStatusCode,
	            WDM.Id WageDetuctionMethodId,
	            WDM.Name WageDetuctionMethodName,
	            WDM.Code WageDetuctionMethodCode,
	            DT.Name DraftTypeName,
	            DT.Id DraftTypeId,
	            BT.Id BrokerTypeId,
	            BT.Name BrokerTypeName,
	            C.Id CustomerId,
	            C.Name CustomerName,
	            PT.Id PaymentTypeId,
	            PT.Name PaymentTypeName,
                TD.ValorDate
            from TrustyDraft.TrustyDraft TD
            left join Bank.Branch BR on BR.Id = TD.BranchId and BR.ActiveStatusId<>3
            left join Bank.Broker BRK on BRK.Id = TD.BrokerId and BRK.ActiveStatusId<>3
            join IssueManagement.Issue I on I.Id = td.IssueId  and I.ActiveStatusId <>3
            join Project.Step S on S.Id = I.CurrenStepId and S.ActiveStatusId <>3
            inner join Authentication.Users U on U.Id = td.CreatedBy and U.ActiveStatusId<>3
            inner join Authentication.Profile P On P.Id = U.ProfileID and p.ActiveStatusId<>3
            left join TrustyDraft.DraftType DT on DT.Id = TD.DraftTypeId and DT.ActiveStatusId <> 3
            left join Bank.CurrencyType CT on CT.Id = TD.DraftCurrencyTypeId and CT.ActiveStatusId<>3
            left join TrustyDraft.DraftStatus DS on DS.Id = TD.DraftStatusId and DS.ActiveStatusId<>3
            left join TrustyDraft.DraftValorStatus DVS on DVS.Id = TD.DraftValorStatusId and DVS.ActiveStatusId<>3
            left join TrustyDraft.WageDeductionMethod WDM on WDM.Id = TD.WageDeductionMethodId and WDM.ActiveStatusId<>3
            left join Bank.BrokerType BT on BT.Id = TD.BrokerTypeId and BT.ActiveStatusId <> 3
            left join Bank.PaymentType PT on PT.Id = TD.PaymentTypeId and PT.ActiveStatusId <> 3
            join Bank.Customer C on C.Id = TD.CustomerId and C.ActiveStatusId <> 3
           where (TD.BrokerId is null or TD.BrokerId = @brokerId) and s.Id = 7275625281  
"; /// s.Id = 7275625281   برای این شرط طبقه گفته تیم تحلیل فقط داده هایی در ارجاع به کارگزاری نمایش داده می شود که گام فرآیند ان ارجاع به کارگزاری باشد
            using var multi = await connection.QueryMultipleAsync(query, new { brokerId = request.BrokerId });
            var response = await multi.ReadAsync<GetAllTrustyDraftRequestedResult>();
            return Result.Ok(response);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<string?> GetLastBranchLetterNumber()
    {
        var query = @"
select top 1 BranchLetterNumber from TrustyDraft.TrustyDraft
where BranchLetterNumber is not null and ActiveStatusId<>3
order by CreatedAt desc
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<string>(query);
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> GetAllFrorEachDepartment(GetAllFrorEachDepartment request)
    {
        var mainQuery = @"
select  
    TD.Id TrustyDraftId,
    TD.DraftNumber,
    TD.DraftNumberBasedOnOrder,
	TD.BranchId,
	BR.Name BranchName,
	BR.Code BranchCode,
	TD.BrokerId,
	BRK.Name BrokerName,
	BRK.Code BrokerCode,
    TD.DraftIssueDate,
	td.IssueId DraftIssueId,
    i.Code DraftIssueCode,
	i.CurrenStepId DraftIssueCurrentStepId,
	s.Name DraftIssueCurrentStepName,
    TD.DraftRequestAmount,
    TD.DraftRequestAmountBasedOnUsd,
    TD.DraftNetAmount,
    TD.DraftRequestNetAmountBasedOnUsd,
    TD.PayingBankName,
    TD.InterMediateBankName IntermediateBankName,
	td.CreatedAt,
	(p.FirstName + ' ' + p.LastName) CreatedBy,
	TD.DraftCurrencyTypeId CurrencyTypeId,
	CT.Name CurrencyTypeName,
	CT.Code CurrencyTypeCode,
	TD.DraftStatusId,
	DS.Name DraftStatusName,
	DS.Code DraftStatusCode,
	TD.DraftValorStatusId,
	DVS.Name DraftValorStatusName,
	DVS.Code DraftValorStatusCode,
	WDM.Id WageDetuctionMethodId,
	WDM.Name WageDetuctionMethodName,
	WDM.Code WageDetuctionMethodCode,
	DT.Name DraftTypeName,
	DT.Id DraftTypeId,
	BT.Id BrokerTypeId,
	BT.Name BrokerTypeName,
	C.Id CustomerId,
	C.Name CustomerName,
	PT.Id PaymentTypeId,
	PT.Name PaymentTypeName,
    TD.ValorDate,
    TD.BranchLetterNumber,
	d.Id
from TrustyDraft.TrustyDraft TD
left join Bank.Branch BR on BR.Id = TD.BranchId and BR.ActiveStatusId<>3
left join Bank.Broker BRK on BRK.Id = TD.BrokerId and BRK.ActiveStatusId<>3
Inner join IssueManagement.Issue I on I.Id = td.IssueId  and I.ActiveStatusId <>3
Inner join Project.Step S on S.Id = I.CurrenStepId and S.ActiveStatusId <>3
inner join Authentication.Users U on U.Id = td.CreatedBy and U.ActiveStatusId<>3
inner join Authentication.Profile P On P.Id = U.ProfileID and p.ActiveStatusId<>3
left join TrustyDraft.DraftType DT on DT.Id = TD.DraftTypeId and DT.ActiveStatusId <> 3
left join Bank.CurrencyType CT on CT.Id = TD.DraftCurrencyTypeId and CT.ActiveStatusId<>3
left join TrustyDraft.DraftStatus DS on DS.Id = TD.DraftStatusId and DS.ActiveStatusId<>3
left join TrustyDraft.DraftValorStatus DVS on DVS.Id = TD.DraftValorStatusId and DVS.ActiveStatusId<>3
left join TrustyDraft.WageDeductionMethod WDM on WDM.Id = TD.WageDeductionMethodId and WDM.ActiveStatusId<>3
left join Bank.BrokerType BT on BT.Id = TD.BrokerTypeId and BT.ActiveStatusId <> 3
left join Bank.PaymentType PT on PT.Id = TD.PaymentTypeId and PT.ActiveStatusId <> 3
join Bank.Customer C on C.Id = TD.CustomerId and C.ActiveStatusId <> 3
inner join Organization.Company Co on Co.Id = U.CompanyId and Co.ActiveStatusId<>3
inner join Organization.Staff Staff on Staff.ProfileId = P.Id and Staff.ActiveStatusId<>3
inner join Organization.Position PO on PO.Id = Staff.PositionId and Po.ActiveStatusId <> 3
inner join Organization.Department D on D.Id = PO.DepartmentId and D.ActiveStatusId<>3
where D.Id = Organization.GetUserDepartment(@UserId)
";
        var query = $@" WITH Query as({mainQuery})
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
        var queryCount = $@" WITH Query as({mainQuery})
                                SELECT Count(*) FROM Query
								 /**where**/";

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        dynaimcParameters.Item2.Add("UserId", _simaIdentity.UserId);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetAllTrustyDraftRequestedResult>();
        return Result.Ok(response, request, count);

    }

    public async Task<long> GetBranchIdByUserId(long userId)
    {
        var query = "select dbo.FN_GetBranchIdByUserId(@UserId)";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<long>(query, new { UserId = userId });
    }

    public async Task<long> GetBrokerTypeIdFromInquiryRequestId(long inquiryRequestId)
    {
        var query = @"
select isnull(b.BrokerTypeId,0) from TrustyDraft.InquiryRequest ir
inner join TrustyDraft.InquiryResponse IRes on IRes.InquiryRequestId = ir.Id
inner join Bank.Broker B on b.Id = IRes.BrokerId and b.ActiveStatusId<>3 and ir.ActiveStatusId<>3
where ir.Id = @InquiryRequestId
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<long>(query, new { InquiryRequestId = inquiryRequestId });
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftsQueryResult>>> GetAllMy(GetAllMyTrustyDraftsQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        string queryCount = $@" WITH Query as(
						                    {_mainQuery} AND TD.CreatedBy = @UserId
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


        string query = $@" WITH Query as(
							                  {_mainQuery} AND TD.CreatedBy = @UserId
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        dynaimcParameters.Item2.Add("UserId", _simaIdentity.UserId);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetAllTrustyDraftsQueryResult>();
        return Result.Ok(response, request, count);
    }
}