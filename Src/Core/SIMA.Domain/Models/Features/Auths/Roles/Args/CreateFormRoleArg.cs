namespace SIMA.Domain.Models.Features.Auths.Roles.Args
{
    public class CreateFormRoleArg
    {
        public long FormId { get; set; }
        public long? RoleId { get; set; }
        public long ActiveStatusId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
