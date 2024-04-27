using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Forms;

public class ModifyFormCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
      public string? JsonContent { get; set; }
}
