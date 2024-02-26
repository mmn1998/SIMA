using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.PhoneTypes;

public class CreatePhoneTypeCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }

    public string? Code { get; set; }
}
