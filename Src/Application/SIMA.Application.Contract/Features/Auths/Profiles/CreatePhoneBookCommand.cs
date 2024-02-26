using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Profiles;
public class CreatePhoneBookCommand : ICommand<Result<long>>
{
    public long ProfileId { get; set; }

    public long? PhoneTypeId { get; set; }

    public string? PhoneNumber { get; set; }
    public long? ModifiedBy { get; set; }



}
