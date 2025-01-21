using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.Resources;

public class DeleteResourceCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}