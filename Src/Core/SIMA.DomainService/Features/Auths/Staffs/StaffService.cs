using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Domain.Models.Features.Auths.Staffs.Interfaces;
using SIMA.Persistance.Read.Repositories.Features.Auths.Staffs;

namespace SIMA.DomainService.Features.Auths.Staffs;

public class StaffService : IStaffService
{
    private readonly IStaffQueryRepository _repository;
    private readonly string _connectionString;

    public StaffService(IStaffQueryRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<bool> IsStaffSatisfied(long profileId, long positionId)
    {
        return await _repository.IsStaffSatisfied(profileId, positionId);
    }

    public async Task<bool> IsPositionDuplicated(long positionId, long profileId)
    {
        var result = false;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
IF EXISTS(SELECT 1 FROM Organization.Staff WHERE (ProfileId = @ProfileId) AND (PositionId = @PositionId))
	BEGIN
        SELECT  1
	END
  ELse
	Begin
		SELECT 0 
	END
";
            var dbResult = await connection.QueryFirstOrDefaultAsync<int>(query, new { PositionId = positionId, ProfileId = profileId });
            if (dbResult == 1) result = true;
            else result = false;
        }
        return result;
    }
}
