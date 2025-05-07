using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BranchManagement.CurrencyTypes;

public class ModifyCurrencyTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string IsBaseCurrency { get; set; }
    public string Symbol { get; set; }
}
