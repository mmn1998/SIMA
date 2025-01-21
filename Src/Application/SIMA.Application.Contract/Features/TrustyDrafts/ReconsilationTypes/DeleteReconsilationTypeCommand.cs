using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.ReconsilationTypes;

public class DeleteReconsilationTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}