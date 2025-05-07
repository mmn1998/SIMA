using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.Auths.UIInputElements;

public class DeleteUIInputElementCommand: ICommand<Result<long>>
{
    public long Id { get; set; }
}
