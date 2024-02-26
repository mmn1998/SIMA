using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;
using System.Windows.Input;

namespace SIMA.Application.Contract.Features.Auths.Profiles;

public class ModifyPhoneBookCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long ProfileId { get; set; }
    public long? PhoneTypeId { get; set; }
    public string? PhoneNumber { get; set; }
    public long ActiveStatusId { get; set; }
}
