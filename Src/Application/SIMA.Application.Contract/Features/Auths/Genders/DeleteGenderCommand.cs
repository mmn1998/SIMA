using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Genders;
public class DeleteGenderCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}