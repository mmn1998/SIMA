using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.ResponsibilityWageTypes;

public class DeleteResponsibilityWageTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}