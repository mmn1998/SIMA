using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.DMS.Documents;

public class MultiCreateDocumentCommand : ICommand<Result<List<long>>>
{
    public List<CreateDocumentCommand> Documents { get; set; }
}
