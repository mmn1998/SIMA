namespace SIMA.Domain.Models.Features.Auths.Users.Args
{
    public class ChangePasswordArg
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string IsFirstLogin { get; set; }
        public DateTime? ChangePasswordDate { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
