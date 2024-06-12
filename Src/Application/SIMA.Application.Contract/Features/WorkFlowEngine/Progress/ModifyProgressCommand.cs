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
        public long? StateId { get; set; }
        public string? ConditionExpression { get; set; }
        public List<ProgressStoreProcedureCommand> StoreProcedures { get; set; }
    }
    public class ProgressStoreProcedureCommand
    {
        public string StoreProcedureName { get; set; }
        public List<ProgressStoreProcedureParamCommand> Params { get; set; }
    }
    public class ProgressStoreProcedureParamCommand
    {
        public string Name { get; set; }
        public long DataTypeId { get; set; }
        public string IsRequierd { get; set; }
    }
}
