using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Staffs;

public class DeleteStaffCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
