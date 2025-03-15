using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.DataProcedureTypes;

public class ModifyDataProcedureTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}