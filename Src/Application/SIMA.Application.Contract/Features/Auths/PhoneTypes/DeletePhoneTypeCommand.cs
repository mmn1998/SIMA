using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.PhoneTypes;

public class DeletePhoneTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
