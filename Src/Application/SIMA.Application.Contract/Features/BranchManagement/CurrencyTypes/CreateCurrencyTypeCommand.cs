using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BranchManagement.CurrencyTypes
{
    public class CreateCurrencyTypeCommand : ICommand<Result<long>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string IsBaseCurrency { get; set; }
    }
}
