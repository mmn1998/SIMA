using Flurl.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Resources;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<long> GetHighestPriority()
        {
            var result = await _context.IssuePriorities.OrderBy(it => it.Ordering).FirstOrDefaultAsync();
            return result.Id.Value;
        }

        public async Task<Issue> GetIssueBySourceId(long sourceId, MainAggregateEnums mainAggregate)
        {
            var result = await _context.Issues.FirstOrDefaultAsync(x=>x.SourceId == sourceId && x.MainAggregateId == new MainAggregateId((long)mainAggregate));
            result.NullCheck();
            return result;
        }

        public async Task<(long, int)> GetIssueMiddleWeight()
        {
            var orderedQuery = _context.IssueWeightCategories.OrderBy(it => it.MinRange);

            int count = orderedQuery.Count();
            if (count == 0)
            {
                throw new SimaResultException(CodeMessges._100065Code, Messages.NoMiddleIssueWeightFound);
            }

            int middleIndex = count / 2;

            double median;
            if (count % 2 == 0)
            {
                median = (orderedQuery.Skip(middleIndex - 1).First().MinRange + orderedQuery.Skip(middleIndex).First().MinRange) / 2.0;
            }
            else
            {
                median = orderedQuery.Skip(middleIndex).First().MinRange;
            }
            var res = orderedQuery.FirstOrDefaultAsync(i => i.MinRange == median);
            if (res != null)
                return (res.Result.Id.Value, res.Result.MinRange);
            return (0, 0);
        }

        public async Task<long> GetIssueTypeRequest()
        {
            var result = await _context.IssueTypes.FirstOrDefaultAsync(it => it.Name.Trim() == "درخواست");
            return result.Id.Value;
        }

        public async Task<Issue> GetLastIssue()
        {
            var result = await _context.Issues.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
            return result;
        }
    }
}
