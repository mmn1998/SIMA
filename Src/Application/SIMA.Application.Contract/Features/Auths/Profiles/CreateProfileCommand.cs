using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Profiles;

public class CreateProfileCommand : ICommand<Result<long>>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FatherName { get; set; }

    public long? GenderId { get; set; }

    public string? NationalId { get; set; }

    public DateOnly? Brithday { get; set; }
}
