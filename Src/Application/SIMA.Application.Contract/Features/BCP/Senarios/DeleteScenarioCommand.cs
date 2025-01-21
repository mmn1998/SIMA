using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.Senarios
{
    public class DeleteScenarioCommand :  ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
