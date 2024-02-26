using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowCompany
{
    public class ModifyWorkFlowCompanyCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long WorkFlowId { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
    }
}
