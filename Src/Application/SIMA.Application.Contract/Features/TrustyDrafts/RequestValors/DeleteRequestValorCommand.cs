using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.RequestValors;

public class DeleteRequestValorCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}