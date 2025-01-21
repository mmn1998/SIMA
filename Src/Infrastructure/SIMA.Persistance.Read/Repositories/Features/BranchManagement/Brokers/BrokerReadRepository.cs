using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.Brokers;

public class BrokerReadRepository : IBrokerReadRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuey;
    public BrokerReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuey = @"
SELECT  B.[Id]
       ,B.[Name]
       ,B.[Code]
       ,B.[BrokerTypeId]
       ,B.[ActiveStatusId]
       ,B.[ExpireDate]
       ,S.[Name] as ActiveStatus
       ,b.[CreatedAt]
	   ,BT.Name BrokerTypeName
	   ,(P.FirstName + ' ' + p.LastName) CreatedBy
FROM [Bank].[Broker] B
Inner join Bank.BrokerType BT on BT.Id = b.BrokerTypeId and bt.ActiveStatusId<>3
INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
INNER Join Authentication.Users U on U.Id = b.CreatedBy and u.ActiveStatusId<>3
INNER JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
WHERE  B.ActiveStatusId != 3
";
    }

    public async Task<Result<IEnumerable<GetBrokerQueryResult>>> GetAll(GetAllBrokerQuery request)
    {
        using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync();

        string queryCount = $@" WITH Query as({_mainQuey})
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

        string query = $@" WITH Query as({_mainQuey})
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";

        var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetBrokerQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<IEnumerable<GetBrokerQueryResult>> GetAllByBrokerTypeId(long brokerTypeId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = $@"
                    {_mainQuey} AND B.[BrokerTypeId] = @BrokerTypeId
";
        return await connection.QueryAsync<GetBrokerQueryResult>(query, new { BrokerTypeId = brokerTypeId });
    }

    public async Task<GetBrokerQueryResult> GetById(long id)
    {
        using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync();
        string query = $@"{_mainQuey}
                            And B.[Id] = @Id

------- BrokerPhoneBook
SELECT BPB.PhoneNumber
		,BPB.PhoneTypeId
		,PT.Name PhoneTypeName
		,B.CreatedAt
	   ,(P.FatherName + ' ' + p.LastName) CreatedBy
FROM [Bank].[Broker] B
Inner join Bank.BrokerPhoneBook BPB on BPB.BrokerId = b.Id and BPB.ActiveStatusId<>3
Inner join Basic.PhonType PT on PT.Id = BPB.PhoneTypeId and PT.ActiveStatusId<>3
INNER Join Authentication.Users U on U.Id = b.CreatedBy and u.ActiveStatusId<>3
INNER JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
WHERE  B.ActiveStatusId != 3 And B.Id = @Id


------ BrokerAddressBook
SELECT BAB.Address
		,BAB.AddressTypeId
		,AT.Name AddressTypeName
		,B.CreatedAt
	   ,(P.FatherName + ' ' + p.LastName) CreatedBy
FROM [Bank].[Broker] B
Inner join Bank.BrokerAddressBook BAB on BAB.BrokerId = b.Id and BAB.ActiveStatusId<>3
Inner join Basic.AddressType AT on AT.Id = BAB.AddressTypeId and AT.ActiveStatusId<>3
INNER Join Authentication.Users U on U.Id = b.CreatedBy and u.ActiveStatusId<>3
INNER JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
WHERE  B.ActiveStatusId != 3 And b.Id = @Id


------ BrokerAccountBook
SELECT BAB.IBANNumber
		,B.CreatedAt
	   ,(P.FatherName + ' ' + p.LastName) CreatedBy
FROM [Bank].[Broker] B
Inner join Bank.BrokerAccountBook BAB on BAB.BrokerId = b.Id and BAB.ActiveStatusId<>3
INNER Join Authentication.Users U on U.Id = b.CreatedBy and u.ActiveStatusId<>3
INNER JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
WHERE  B.ActiveStatusId != 3 And b.Id = @Id

";
        using var multi = await connection.QueryMultipleAsync(query, new { Id = id });
        var result = await multi.ReadFirstOrDefaultAsync<GetBrokerQueryResult>();
        result.NullCheck();
        result.BrokerPhoneBookList = await multi.ReadAsync<GetBrokerPhoneBookQueryResult>();
        result.BrokerAddressBookList = await multi.ReadAsync<GetBrokerAddressBookQueryResult>();
        result.BrokerAccountBookList = await multi.ReadAsync<GetBrokerAccountBookQueryResult>();
        return result;

    }
}
