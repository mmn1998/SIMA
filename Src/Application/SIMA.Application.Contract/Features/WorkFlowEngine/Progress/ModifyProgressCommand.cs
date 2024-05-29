using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.Progress
{
    public class ModifyProgressCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public long StateId { get; set; }
        public string ConditionExpression { get; set; }
    }
}
