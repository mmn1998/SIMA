using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Profiles;
public class DeleteProfileCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}