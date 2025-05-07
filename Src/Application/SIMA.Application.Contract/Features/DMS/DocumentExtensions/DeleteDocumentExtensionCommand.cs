using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.DMS.DocumentExtensions;

public class DeleteDocumentExtensionCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
