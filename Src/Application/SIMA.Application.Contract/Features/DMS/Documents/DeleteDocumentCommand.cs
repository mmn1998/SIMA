using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.DMS.Documents;

public class DeleteDocumentCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
