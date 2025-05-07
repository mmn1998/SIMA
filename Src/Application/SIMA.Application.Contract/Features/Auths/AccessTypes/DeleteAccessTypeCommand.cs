using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.AccessTypes;

public class DeleteAccessTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}