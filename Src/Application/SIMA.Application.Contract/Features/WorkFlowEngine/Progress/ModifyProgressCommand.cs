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
        public List<ProgressStoreProcedureCommand>? StoreProcedures { get; set; }
    }
    public class ProgressStoreProcedureCommand
    {
        public long? Id { get; set; }
        public string StoreProcedureName { get; set; }
        public float ExecutionOrdering { get; set; }
        public List<ProgressStoreProcedureParamCommand>? Params { get; set; }
    }
    public class ProgressStoreProcedureParamCommand
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public long DataTypeId { get; set; }
        public string IsRequired { get; set; }
        public string IsSystemParam { get; set; }
        public string? SystemParamName { get; set; }
        public string DisplayName { get; set; }
        public string JsonFormat { get; set; }
        public string? BoundFormat { get; set; }
        public string? ApiNameForDataBounding { get; set; }
        public string? StoredProcedureForDataBounding { get; set; }
        public long? UiInputElementId { get; set; }
        public long? ApiMethodActionId { get; set; }

    }
}
