using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.DraftDestinations
{
    public class CreateDraftDestinationCommand : ICommand<Result<long>>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
