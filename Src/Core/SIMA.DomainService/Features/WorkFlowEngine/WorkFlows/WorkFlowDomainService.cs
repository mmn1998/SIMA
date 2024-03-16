using Microsoft.EntityFrameworkCore;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Security;
using SIMA.Persistance.Persistence;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace SIMA.DomainService.Features.WorkFlowEngine.WorkFlows
{
    public class WorkFlowDomainService : IWorkFlowDomainService
    {
        private readonly SIMADBContext _context;
        private readonly ISimaIdentity _simaIdentity;

        private readonly string _connectionString;

        public WorkFlowDomainService(IConfiguration configuration, SIMADBContext context, ISimaIdentity simaIdentity)
        {
            _context = context;
            _simaIdentity = simaIdentity;
            _connectionString = configuration.GetConnectionString();
        }

        public async Task<bool> CheckWorkFlow(long workflowId)
        {
            bool result = false;

            if (workflowId != 0)
                result = await _context.WorkFlows.AnyAsync(x => x.Id == new WorkFlowId(workflowId));

            return result;
        }

        public async Task<bool> IsCodeUnique(string code, long id)
        {
            if (id > 0)
                return await _context.WorkFlows.AnyAsync(b => b.Code == code && b.Id != new WorkFlowId(id));
            else
            {
                var result = await _context.WorkFlows.AnyAsync(b => b.Code == code);
                return result;
            }
        }

        public async Task<bool> SteteIsCodeUnique(string code, long id)
        {
            if (id > 0)
                return await _context.WorkFlows.SelectMany(x => x.States).AnyAsync(b => b.Code == code && b.Id != new StateId(id));
            else
            {
                var result = await _context.WorkFlows.SelectMany(x => x.States).AnyAsync(b => b.Code == code);
                return result;
            }
        }

        public async Task<bool> CheckCreateIssueWithActor(long workflowId)
        {
            var userid = _simaIdentity.UserId;
            var groups = _simaIdentity.GroupId;
            var roles = _simaIdentity.RoleIds;

            var queryString = @"select Count(*) Result from Project.WorkFlow w 
                                join Project.WorkFlowActor a on a.WorkFlowId=w.Id
                                join Project.Step s on s.WorkFlowID=w.Id
                                left join Project.WorkFlowActorUser u on u.WorkFlowActorId=a.Id
                                left join Project.WorkFlowActorRole r on r.WorkFlowActorID = a.Id
                                left join Project.WorkFlowActorGroup g on g.WorkFlowActorID = a.Id
                                where w.Id=@workflowId and s.ActionTypeId=9 and (u.UserID = @userid or r.RoleID in @roles or g.GroupID in @groups);"
            ;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<int>(queryString, new { workflowId, userid, roles, groups });
                return result != 0;
            }
        }
    }
}
