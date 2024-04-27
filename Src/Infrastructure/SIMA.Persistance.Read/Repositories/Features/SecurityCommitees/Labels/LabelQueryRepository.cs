using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Persistance.Persistence;
using SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.MeetingHoldingStatus;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.Labels
{
    public class LabelQueryRepository : ILabelQueryRepository
    {
        private readonly string _connectionString;
        private readonly SIMADBContext _context;

        public LabelQueryRepository(IConfiguration configuration, SIMADBContext context)
        {
            _connectionString = configuration.GetConnectionString();
            _context = context;
        }

       
    }
}
