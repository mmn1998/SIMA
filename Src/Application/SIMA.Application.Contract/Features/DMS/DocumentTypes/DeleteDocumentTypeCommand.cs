using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.DMS.DocumentTypes;

public class DeleteDocumentTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
