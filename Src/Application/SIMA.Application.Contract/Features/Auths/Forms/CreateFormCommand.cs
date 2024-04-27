using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Forms;

public class CreateFormCommand : ICommand<Result<long>>
{
    public long DomainId { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
    public string? Code { get; set; }
    public string? IsSystemForm { get; set; }
}