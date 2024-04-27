using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.Labels;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.Meetings
{
    public class MeetingQueryRepository : IMeetingQueryRepository
    {
        private readonly string _connectionString;
        public MeetingQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }

        public async Task<List<GetLabelResult>> GetLabelByCode(List<string> labels)
        {
            try
            {
                string query = $@"
                    select L.[Id] , L.[Code] from SecurityCommitee.Label L
                     where (L.Code IN @Label) and L.ActiveStatusId <> 3
                    ";
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryAsync<GetLabelResult>(query, new { Label = labels });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
