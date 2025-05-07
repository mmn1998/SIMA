using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.ImportanceDegrees;

public class DeleteImportanceDegreeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
