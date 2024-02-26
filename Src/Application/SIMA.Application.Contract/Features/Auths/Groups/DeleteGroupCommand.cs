using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Groups;
public class DeleteGroupCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}