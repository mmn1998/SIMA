using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BranchManagement.BranchTypes
{
    public class DeleteBranchTypeCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
