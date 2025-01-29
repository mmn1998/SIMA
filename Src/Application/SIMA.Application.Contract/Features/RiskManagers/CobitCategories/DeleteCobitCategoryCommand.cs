using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.CobitCategories;

public class DeleteCobitCategoryCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}