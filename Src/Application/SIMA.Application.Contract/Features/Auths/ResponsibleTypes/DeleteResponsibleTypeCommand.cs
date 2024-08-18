using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.ResponsibleTypes;

public class DeleteResponsibleTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
