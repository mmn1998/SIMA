using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.AssetCustomFields
{
    public class DeleteAssetCustomFieldCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
