using Microsoft.Extensions.Configuration;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Interface;
using SIMA.Framework.Common.Security;
using SIMA.Persistance.Persistence;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlow;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlowActor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.DomainService.Features.WorkFlowEngine.WorkFlowActor
{
    public class WorkFlowActorDomainService : IWorkFlowActorDomainService
    {
        private readonly IWorkFlowActorQueryRepository _workFlowActorQueryRepository;

        public WorkFlowActorDomainService(IWorkFlowActorQueryRepository workFlowActorQueryRepository)
        {
            _workFlowActorQueryRepository = workFlowActorQueryRepository;
        }
        public async Task<bool> IsAccessToEveryOne(long Id)
        {
            return await _workFlowActorQueryRepository.CheckAccessToIsEveryOne(Id);
        }

    }
}
