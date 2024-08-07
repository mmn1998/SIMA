using System.Text.Json.Serialization;

namespace SIMA.Domain.Models.Features.Auths.Users.Args;

public class CreateUserArg
{
    public long Id { get; set; }
    public long ProfileId { get; set; }
    public long CompanyId { get; set; }
    [JsonIgnore]
    public DateOnly ActiveFrom { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public long ActiveStatusId { get; set; }
    public string IsFirstLogin { get; set; }
    public string IsLocked { get; set; }

}
