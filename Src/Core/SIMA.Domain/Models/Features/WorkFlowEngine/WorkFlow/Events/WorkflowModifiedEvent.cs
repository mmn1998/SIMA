using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Events
{
    public record WorkflowModifiedEvent(List<StepId> DeactiveStepIds, long userId) : IDomainEvent;
}
