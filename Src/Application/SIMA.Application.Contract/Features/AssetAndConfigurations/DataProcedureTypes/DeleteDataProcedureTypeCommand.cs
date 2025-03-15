using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.DataProcedureTypes;

public class DeleteDataProcedureTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}