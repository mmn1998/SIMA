using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowCompany
{
    public class CreateWorkFlowCompanyCommand : ICommand<Result<long>>
    {

        public long CompanyId { get; set; }
        public long WorkFlowId { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
    }
}
