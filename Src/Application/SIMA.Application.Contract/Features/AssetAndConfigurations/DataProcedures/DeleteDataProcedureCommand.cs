using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.DataProcedures;

public class DeleteDataProcedureCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}