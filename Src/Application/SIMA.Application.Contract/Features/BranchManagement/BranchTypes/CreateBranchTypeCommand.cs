using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BranchManagement.BranchTypes
{
    public class CreateBranchTypeCommand : ICommand<Result<long>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
