using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;

public class ApiMethodAction
{
    public ApiMethodActionId Id { get; private set; } = new(IdHelper.GenerateUniqueId());
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public ICollection<ProgressStoreProcedureParam>? ProgressStoreProcedureParams { get; set; }
}
