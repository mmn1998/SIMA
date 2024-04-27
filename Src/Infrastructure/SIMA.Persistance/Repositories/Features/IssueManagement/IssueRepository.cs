using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.IssueManagement
{
    public class IssueRepository : Repository<Issue>, IIssueRepository
    {
        private readonly SIMADBContext _context;

        public IssueRepository(SIMADBContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
        }

        public async Task ExcecuteStoreProcedure(string spName)
        {
            //spName = "SpRegisterCustomer @cmbGender = $Value,@nAge = $Value, @txtFirtName = N'$Value', @cmbDegree=$Value";
            await _context.Database.ExecuteSqlAsync($"EXEC {spName}");
        }

        public async Task<Issue> GetById(long id)
        {
            var result = await _context.Issues.FirstOrDefaultAsync(ip => ip.Id == new IssueId(id));
            result.NullCheck();
            return result;
        }

        public async Task<Issue> GetLastIssue()
        {
            var result = await _context.Issues.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
            return result;
        }
    }
}
