using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Categories;

public class DeleteCategoryCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}