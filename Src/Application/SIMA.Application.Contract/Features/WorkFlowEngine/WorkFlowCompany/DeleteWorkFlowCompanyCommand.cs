using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowCompany
{
    public class DeleteWorkFlowCompanyCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
